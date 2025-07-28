# DungeonSlime

Full Phase-Aligned Study Guide for Building a 2D Platformer in MonoGame
References:

GPP – Game Programming Patterns by Robert Nystrom

PDG – 2D Game Development: From Zero to Hero (Pseudocode Edition)

Phase 1 – Foundation
Focus: Game loop, input abstraction, sprite rendering, update architecture

GPP:

Ch. 9 – Game Loop

Ch. 10 – Update Method

Ch. 14 – Component

PDG:

2.3 Structure of this Book – Explains modular engine design

4.5 Programming Languages – Engine abstraction context

4.6 Computers are (not) precise – Time step and float caveats

4.8 Estimating the complexity of algorithms – Core loop design sensitivity

Phase 2 – Player and World Basics
Focus: Movement, jumping, physics, AABB collision, tile logic, animation

GPP:

Ch. 4 – Observer (input system binding, optional)

Ch. 5 – Prototype (tile prefabs or actor cloning)

Ch. 7 – State (player movement FSM)

PDG:

3.9 Vectors – Motion, velocity, acceleration

3.10 Geometry – Bounding boxes, direction

3.14 Coordinate Systems on Computers – UV/screen-space mapping

3.15 Transformation Matrices – For camera logic

4.11 Object-Oriented Programming – Organizing player logic

Phase 3 – Environment and Interactions
Focus: Tilemaps, parallax layers, environmental objects, interactions

GPP:

Ch. 14 – Component

Ch. 15 – Event Queue

Ch. 20 – Spatial Partition

PDG:

4.13 Reading UML diagrams – Object hierarchy planning

4.15.3 Dynamic Arrays – Managing tile layers and dynamic objects

4.2 Basics of Logic – Tile properties, triggers

Phase 4 – Enemies and Game Loop
Focus: Enemy behavior, FSM AI, health/damage, death logic

GPP:

Ch. 7 – State

Ch. 12 – Subclass Sandbox

Ch. 13 – Type Object

PDG:

4.11.4 Inheritance and Polymorphism – Enemy class hierarchy

4.11.9 Composition over Inheritance – Behavior modularity

4.15.1 Graphs – Transition graphs, AI logic flow

4.9 Order of Algorithms – Enemy update performance

Phase 5 – UI and Feedback
Focus: HUD, pause/menu screens, visual/audio feedback

GPP:

Ch. 15 – Event Queue

Ch. 16 – Service Locator

Ch. 18 – Dirty Flag

PDG:

4.13.2 Class Diagrams – Designing UI system

4.17 Truthiness and Falsiness – Conditional HUD display

4.2.3 Logic vs Bitwise Operations – Event/UI flags

4.13 UML – Menu screen object layout

Phase 6 – Tools and Scaling
Focus: Tiled/map editor support, system modularity, clean architecture

GPP:

Ch. 5 – Prototype

Ch. 13 – Type Object

Ch. 16 – Service Locator

PDG:

4.11.12 SOLID Principles – Tool extensibility

4.12 Designing Entities as Data – External level file design

4.19 The Principle of Locality – Engine-wide cache efficiency

Phase 7 – Polish and Finalization
Focus: Performance tuning, asset reuse, build stability

GPP:

Ch. 17 – Data Locality

Ch. 18 – Dirty Flag

Ch. 19 – Object Pool

PDG:

4.20 Multidimensional Structures as 1D – Array/memory layout

4.10 Karnaugh Maps – Optimizing conditional logic

4.15.9 Stacks / 4.15.10 Queues – Input buffering, pooling

4.15.6 Hash Tables – Fast runtime lookups for assets/entities
