#server.py
 
import sys
import socket
import os
 
host = ''
skServer = socket.socket(socket.AF_INET,socket.SOCK_STREAM)
skServer.bind((host,2525))
skServer.listen(10)
print ("Server Active")
bFileFound = 0
while True:
    while True:
        Content,Address = skServer.accept()
        print (Address)
        path_and_file = Content.recv(1000).decode()

        path = os.path.dirname(path_and_file)
        fileName = os.path.basename(path_and_file)

        for file in os.listdir(path):
            if file == fileName:
                bFileFound = 1
                break
    
        if bFileFound == 0:
            print (fileName + " Not Found On Server")
    
        else:
            print (fileName + " File Found")
            fUploadFile = open(path_and_file,"rb+")
            sRead = fUploadFile.read(1000)

            while sRead:
                Content.send(sRead)
                sRead = fUploadFile.read(1000)
            print ("Sending Completed")
        break
    Content.close()

skServer.close()
