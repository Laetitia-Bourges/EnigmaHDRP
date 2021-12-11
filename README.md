# EnigmaHDRP

This project was an exercice during my trainning in gameplay programming.
I had 5 days maximum to finish it

I had to program the encryption and decryption system of the Enigma machine, which was used by the Germans during WWII.

<p align="center">
  <img src="https://user-images.githubusercontent.com/49199773/145694333-0b497f10-c507-4f22-ad42-d600c15077b9.png" width="400">
  <img src="https://user-images.githubusercontent.com/49199773/145694351-c750d37c-d2df-427c-9144-48b08ebdb4d2.png" width="400">
</p>

There is a system with 3 rotors where each of them encrypts a letter according to a specific pattern, and there was an encoding key to decrypt the message

1 - Crypt the message with an encoding key : 
<p align="left">
  <img src="https://user-images.githubusercontent.com/49199773/145694374-7ee9d590-40bc-4ecc-ad06-c1418b2326ca.png" width="500">
</p>

2 - Decrypt the message with the same encoding key :
<p align="left">
  <img src="https://user-images.githubusercontent.com/49199773/145694368-a89bc056-2c85-4d45-ba47-a033a4787eb2.png" width="500">
</p>

3 - Decrypt a message with the wrong encoding key does not decrypt the message : 
<p align="left">
  <img src="https://user-images.githubusercontent.com/49199773/145694386-2ace23be-6c3f-4752-8e87-8229c32f08f3.png" width="500">
</p>

This project uses the JsonUtility library to retrieve the patterns of the rotors and to create a file containing the message encrypted on the PC when the COPY button is pressed.

Assets and sound are free of rights or were given by my teacher
The scene to start is Asset/Scenes/MainScene
