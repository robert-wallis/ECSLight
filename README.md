# ECS Light

ECS Light is a lightweight Entity Component System ([ECS](https://en.wikipedia.org/wiki/Entity%E2%80%93component%E2%80%93system)).

The main design principles of this library should allow users to:

* Favor Composition over Inheritance
* Seperate Behavior and State
* Easy to Test, and Refactor your code
* Enable Clean-Code such as IoC

To that end, ECS Light does not do any code generation.  We found that
ECS code generators add very little and make developers fear refactoring code.

ECS Light is a portable library so it can be used easily in Unity, MonoGame (XNA), or 
on a headless server.

We try to avoid forcing the user to use a certain paradigm.  Other than the ECS paradigm of course!
For example, Contexts are not in a Singleton, therefore a headless server can run multiple
seperate contexts.  Or two players can play splitscreen with different contexts for each player.
The user is of course free to put a Context into a singleton themself.  (We think
singletons often allow code to violate dependency inversion, making it harder to test.)

We don't claim ECS Light is the prime example of Clean-Code.  Surely it violates some SOLID principles.
Please submit a pull-request if you would like to refactor ECS Light into a cleaner, faster library for everyone.
