# Polygon Editor - WinForms Application

## Overview

This is a WinForms (C#) application for editing both standard polygons and curved polygons with Bézier segments. 
The editor allows creating, modifying, and constraining polygonal shapes with various geometric relationships and continuity options.

## Features

### Basic Polygon Operations
- Create and delete polygons
- Edit existing polygons with multiple operations:
  - Move vertices/Bézier control points
  - Delete vertices
  - Add new vertices at edge midpoints
  - Move the entire polygon

### Edge Constraints
 - Apply geometric constraints to edges:
   - Horizontal edge
   - Vertical edge
   - Fixed edge length
- Visual indicators for applied constraints
- Remove existing constraints

### Bézier Curve Support
 - Toggle edges between straight segments and cubic Bézier curves (3rd degree)
 - Visual display of Bézier control polygon (dashed line) with 2 additional control points
 - Vertex continuity options where edges meet:
   - G0 continuity (positional continuity)
   - G1 continuity (unit tangent vector continuity)
   - C1 continuity (tangent vector continuity)

### Drawing Algorithms
 - Library implementation for line drawing
 - Custom Bresenham's algorithm implementation

### Interaction
 - Left mouse button drag to move:
   - Vertices
   - Control points
   - Entire polygons
 - Selected vertex/control point follows mouse cursor precisely
 - Switch between Bresenham and library algorithms with a radiobutton

## Usage Instructions

### Creating and Editing Polygons
 - Use the toolbar to the right to create a new polygon or delete the existing one
 - Select a vertice to delete it or select an edge to add a vertice in the middle

### Applying Constraints
1. Select an edge
2. Choose a constraint from the constraints menu:
   - Horizontal
   - Vertical
   - Fixed length (with dialog to specify length)
3. Constraints appear as visual indicators near the edge

### Working with Bézier Curves
1. Select an edge
2. Select "Bézier Curve" to convert between straight and curved
3. Adjust Bézier control points to shape the curve
4. Set continuity options at connecting vertices by selecting them and choosing the continuity

