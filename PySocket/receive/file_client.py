#client.py
 
import sys
import socket 
import os
 
skClient = socket.socket(socket.AF_INET, socket.SOCK_STREAM)
skClient.connect(("127.0.0.1",2525))
 
sFileName = input("Enter Filename to download from server : ")
sData = "Temp"
 
while True:
    skClient.send(sFileName.encode())
    sData = skClient.recv(1000)
    sFileName = os.path.basename(sFileName)
    fDownloadFile = open(sFileName,"wb")
    while sData:
        print(len(sData))
        fDownloadFile.write(sData)
        sData = skClient.recv(1000)
    print ("Download Completed")
    break
 