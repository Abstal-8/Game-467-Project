VAR metBefore = false

-> cat_menu

=== cat_menu ===
{ metBefore:
    We meet again, human.
- else:
    Hello, traveler.
    ~ metBefore = true
}

* "What are you?"
    I am a cat. Obviously.
    -> cat_menu

* "Why are you locked up?"
    They'll be coming back soon are you sure you want to waste time asking silly questions
    -> cat_menu
* "How can I help you?"
    I think I saw one of them put a key behind that wall there, don't know how you will get accross though, maybe if you had a spirit form and were able to swap postions with it by pressing 'F'
    -> DONE
