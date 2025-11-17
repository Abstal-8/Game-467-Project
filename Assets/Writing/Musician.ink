//Player enters Music Room and finds the Musician hunched over the piano playing his tune.

- Musician: Hm? You startled me... 
- Musician: I thought only the Doctor came here at this hour.
- Musician: A great pianist like myself misplaced their sheet music.
- Musician: I'm forced to improvise. He'll be furious if I falter.

* [Why would you falter, you already play so well.] -> Kind
* [Who's this Doctor you speak of?] -> Curious
* [A great pianist would have memorized the melody.] -> Argumentative

=== Kind === 
- Musician: You flatter me, but my mind is all over the place. 
- Musician: Without my notes, I can only recall fragments.
- Musician: The Doctor says my anger clouds the melody. 
- Musician: That I must continue playing until I am rid of it.

* [Maybe I can help.] -> Kind_2
* [Sorry, I have more pressing matters.] -> Argumentative
-> DONE

=== Curious ===
- Musician: The Head Doctor has many patients.
- Musician: He often runs experiments on them, myself included.

* [Why would you falter, you already play so well.] -> Kind
* [Don't you think a great pianist would have memorized the melody.] -> Argumentative

-> DONE

=== Argumentative === // +1 Bad Ending
- Musician: How dare you insult me?!

-> Argumentative_Battle

=== Kind_2 === // +1 Good Ending
- Musician: Well then be a dear and find my lost pages would you?

* [Where's a good place to start?] -> Quest_Info1
* [How many pages are there?] -> Quest_Info2
* [I can do that.] -> Post_Quest_Giving

-> DONE

=== Quest_Info1 ===
- Musician: I often hide my pages behind paintings.
- Musician: The other patients like to play games with my work.

* [How many pages are there?] -> Quest_Info2
* [I think I have all I need] -> Post_Quest_Giving

-> DONE

=== Quest_Info2 
- Musician: There should be three lying around here somewhere.
- Musician: If you cannot find all three try looking in the Main Living Area.

* [Where's a good place to start?] -> Quest_Info1
* [I think I have all I need] -> Post_Quest_Giving

-> DONE

=== Post_Quest_Giving ===
- Player may end the dialogue and find the pages. Then return to continue the dialogue further

* [leave to find pages] -> END
* [Player returns to Musician] -> Kind_Quest_Unfinished

-> DONE

=== Kind_Quest_Finished === 
//Player returns to the Musician with all 3 pages
- Musician: Ah... Thank you for finding them!
- Musician: Now I can finally play the right melody.
// The Musician plays his melody and then hits a wrong note
- Musician: No no no!
- Musician: Did you hear that?
- Musician: The song of my suffering, the one he made me play.
- Musician: You're working with HIM aren't you?!

* [What are you talking about?] -> Kind_Battle
* [You aren't making any sense] -> Kind_Battle
* [...] -> Kind_Battle

-> DONE

=== Kind_Quest_Unfinished === 
// when player interacts w Musician after quest is given
- Musician: Have you found my pages yet, I need to polish my melody.

* [I have them.] -> Kind_Quest_Finished
* [I need your help.] -> Kind_2
* [Not yet.] -> Post_Quest_Giving


-> DONE

=== Kind_Battle ===
- Musician: DON'T LIE TO ME!!

//The Player and the Musican then proceed to battle each other

-> DONE

=== Argumentative_Battle ===
- Musician: You ought to be taught a lesson!

//The Player and the Musican then proceed to battle each other

-> DONE


