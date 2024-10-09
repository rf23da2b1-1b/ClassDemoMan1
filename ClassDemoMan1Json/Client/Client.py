from socket import *
import json

# slags konstanter
serverName = 'localhost'
serverPort = 10101


clientSocket = socket(AF_INET, SOCK_STREAM) # Stream = tcp
clientSocket.connect( (serverName,serverPort) )


request = {
    "Method" : "",
    "Tal1" : 0,
    "Tal2" : 0
}
# Linje 1
# læser input fra skærm/tastetur
sentence = input('Input Method: ')
request["Method"] = sentence

sentence = input('Input Tal1: ')
request["Tal1"] = int(sentence)

sentence = input('Input tal2: ')
request["Tal2"] = int(sentence)

jsonStr = json.dumps(request)
jsonStr = jsonStr + '\r\n' # '\r\n' er for at lave et linjeskift så det også virker med en c# server
print('Sender: ', jsonStr)

byteSentence = jsonStr.encode() # laver tegn til byte
clientSocket.send(byteSentence)

#venter på svar
returnJsonByte = clientSocket.recv(1024)
returnJson = returnJsonByte.decode() # byte til tegn
print('Modtaget: ', returnJson) 

response = json.loads(returnJson)
print('OK  = ', response["Ok"])
print('Res = ', response["Result"])
print('Err = ', response["ErrorMessage"])

clientSocket.close()