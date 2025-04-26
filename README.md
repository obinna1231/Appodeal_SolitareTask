# Appodeal Test Task

## What I Built

### Cell (Deck)
- A system that holds and manages Items.

### Item (Card)
- An object that can be dragged and dropped onto Cells.
- Items can be stacked together and moved as a group. *(Currently, there is no limit to the number of cards that can be stacked.)*

### Field / Item Manager
- **Field**: Manages the number of Decks (Cells) active in the scene.
- **Item Manager**: Handles the saving, loading, and spawning of Items.

### Buttons
- **Spawn Button**: Spawns a new Item/Card into a random available Cell when clicked.
- **Undo Button**: Reverts the last move. The button displays a counter showing how many moves can be undone.

### GameConfig
- A central configuration hub to tweak game parameters, such as:
  - Cell settings
  - Item settings
  - Animation speeds
  - Drag and drop behaviors
- Access GameConfig by pressing **CTRL + SHIFT + T** in editor or runtime.
- Changes made through GameConfig apply immediately without needing to restart Play Mode.

---

## What I Would Improve
- Implement a modular **Command Pattern** for Undo/Redo actions to extend move operations.
- Add **validation rules** to control how and where Items can be placed, ensuring only legal moves are allowed.
- Improve **input handling** for Item interactions (currently uses basic Raycast for fast prototyping).
- Add **object pooling** for Decks and Cards to optimize memory usage and performance.

---

## Which Parts Were AI-Assisted
- No AI was used in this project.
