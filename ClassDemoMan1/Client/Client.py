from socket import *

# slags konstanter
serverName = 'localhost'
serverPort = 10100


clientSocket = socket(AF_INET, SOCK_STREAM) # Stream = tcp
clientSocket.connect( (serverName,serverPort) )


# Linje 1
# læser input fra skærm/tastetur
sentence = input('Input Method: ')

sentence = sentence + '\r\n' # '\r\n' er for at lave et linjeskift så det også virker med en c# server
byteSentence = sentence.encode() # laver tegn til byte
clientSocket.send(byteSentence)

#venter på svar
returnSentence = clientSocket.recv(1024)
print('Modtaget: ', returnSentence.decode()) # byte til tegn

# Linje 2
# læser input fra skærm/tastetur
sentence = input('Input 2 tal med mellemrum: ')

sentence = sentence + '\r\n' # '\r\n' er for at lave et linjeskift så det også virker med en c# server
byteSentence = sentence.encode() # laver tegn til byte
clientSocket.send(byteSentence)

#venter på svar
returnSentence = clientSocket.recv(1024)
print('Modtaget: ', returnSentence.decode()) # byte til tegn



clientSocket.close()