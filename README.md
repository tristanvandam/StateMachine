# StateMachine
Generic library for a state machine


3 concepts:
Actions, State and Latches.

Actions are what drive a change in state. I.e and event. These are the links/lines in a state diagram.
State is what the current state of the statemachine is. This is generally immutable. 
Latches are a mechanism that holds state and responds to an action. In electronics you get DLatches and RS-Latches for example. The concept has been simplified and is just a latch without the concept of a clock signal to drive the state change such as in electorincs.


//TODO - Readme