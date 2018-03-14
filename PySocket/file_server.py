#server.py
 
import sys
import socket
import os
 
host = ''
skServer = socket.socket(socket.AF_INET,socket.SOCK_STREAM)
skServer.bind((host,2525))
skServer.listen(10)

dir = "C:/Users/KaspersLaptop/Documents/GitHub/IKN-public/PySocket/send/"
print ("Server Active")
bFileFound = 0
 
while True:
    Content,Address = skServer.accept()
    print (Address)
    sFileName = Content.recv(1000).decode()
    for file in os.listdir(dir):
        if file == sFileName:
            bFileFound = 1
            break
 
    if bFileFound == 0:
        print (sFileName + " Not Found On Server")
 
    else:
        print (sFileName + " File Found")
        fUploadFile = open(dir + sFileName,"rb+")
        sRead = fUploadFile.read(1000)

        while sRead:
            Content.send(sRead)
            sRead = fUploadFile.read(1000)
        print ("Sending Completed")

 
Content.close()
skServer.close()