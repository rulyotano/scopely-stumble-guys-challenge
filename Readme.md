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

## Result

**Didn't get offer:** Failed in the technical interview.

**Reason:** Not clear

**My thoughts:**
- The technical interview didn't started well. To me, reviewers didn't payed to much attention to the coding task. They said couldn't run it because missed some files. (the one they included into the task), but I specified in the README.me (this file) that needed to setup it. Also told couldn't run the tests. I don't know how it is possible, tests are passing even in the git pipeline.
- They seem not to be familiar with `Directory.Build.props`, they didn't understood that they can't build each project separately because of this. Maybe this is why they couldn't run the tests.
- Interviewer enter in useless discussion about if common data structure is CrossCutting or not, or some method naming, maybe were not perfect, I got it, but I don't see that as very important point to digging.
- My bad, during the code check I was like exhausted, nervous, I don't know, I was to slow, I wasn't totally focused, I was felling I was giving a bad impression, like if I don't know the code I did 🤷‍♂️.
- One question maybe I din't get the correct answer, was how I know doing the Trie iterative was faster that using recursion. I should have answer by doing benchmarks with big data, and not running it against problem test cases.
- I liked more the design part. They told me how to build an url shortener.
- In the beginning I didn't know from where to start and was to slow.
- The interviewer asked me what http methods to use, what kind of database is best, how do we generate the key.
- In the first moment I was blocked and told by making md5, they told me that is to long. Then I realized that we can generate a long. From the database generation, or from using twitter algorithm to generate unique distributed ids.
- Maybe I pushed hard to this. They we jumped on how to assign ids to servers, Zookeeper, and how zookeeper assigned the ids. I this point I told I didn't know exactly how it do it. But I was not sure about this goal, maybe it was to get into collision topic, but I wasn't sure and didn't want to improvise.
- Then he asked about how to improve a bit more. It was by adding cache.
- Then he asked how to check if urls was already inserted. He wanted to get to the index in sql. But also I took the Bloom Filter (didn't remember the name and I was describing it). He argued that was only probabilistic, but I told him it was to exclude false request to the database, because if the Bloom filter return false we are sure it isn't.
- The interview took more time (half an hour more), to be honest I didn't feel that bad about it. But I know I was to nervous and have a lot of things to improve about it.