# Scopely code challenge
In this code challenge we need to implement a Trie data structure and use it for scrabble like game named ReelWords.

- [PFD Document with specifications](Scopely%20Barcelona%20Studio%20-%20Server%20Engineering%20Challenge.pdf)
- [Whiteboard](https://miro.com/welcomeonboard/MGJTODRmM3NEZ0VZZVZMSVVpblJxQk5mR1FwMUYySnExZmFPN3VFN0wwSDh1RFZ6cnlYelVkQzhkR1BueWNUZHwzNDU4NzY0NTQwNDk3ODg1NTA1fDI=?share_link_id=670345657844)

## Event storming
![Event storming](Event%20storming.jpg)

## Notes

To be able to play the game, you will need to configure the correct path values in the `ReelWords` project's settings (`App.config`). Currently it is configured with a relative path, but it may vary according to the running environment:

- `ValidWordsPath`: path to valid words, in this task `american-english-large.txt`
- `ReelsPath`: path to reels config, in this task `reels.txt`
- `ScoresPath`: path to scores, in this task `scores.txt`