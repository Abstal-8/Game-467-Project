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

* "Are you a cat?"
    Yes. You’re very observant.
    -> cat_menu

* "Why are you locked up?"
    They say I cause trouble. I say I improve the place.
    -> DONE
