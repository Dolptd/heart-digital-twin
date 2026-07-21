# Development of a Heart Digital Twin Method by Using Unity3D

An interactive 3D heart visualization prototype developed from computed tomography data.

The workflow includes medical image segmentation, 3D reconstruction, mesh optimization, and real-time visualization in Unity3D.

## Project Overview

This project aims to develop a prototype framework for generating an interactive heart digital twin from CT scan data.

The main workflow is:

DICOM CT data
→ Segmentation in 3D Slicer
→ 3D reconstruction
→ Mesh cleanup and optimization in Blender
→ Interactive visualization in Unity3D

The current prototype focuses on anatomical visualization and interaction. It is not intended for clinical diagnosis, predictive simulation, or real-time physiological modeling.

## Main Features

- Rotate the 3D heart model
- Zoom in and out
- Pan the camera
- Select individual heart components
- Adjust transparency
- Control simplified heartbeat speed using a BPM slider
- Reset the model and camera to default settings

## Software Requirements

- Unity Hub
- Unity Editor: 6000.5.3f1
- Blender
- 3D Slicer 5.12.0
- Git or GitHub Desktop

The exact Unity Editor version can be found in:

ProjectSettings/ProjectVersion.txt

It is recommended to use the same Unity Editor version to avoid compatibility issues.

## Repository Structure

Assets/
├── Materials/
├── Prefabs/
├── Scenes/
├── Scripts/
└── UI/

Packages/
ProjectSettings/
README.md
.gitignore

The following Unity-generated folders are excluded from the repository:

Library/
Temp/
Obj/
Logs/
UserSettings/
Build/

## Important: 3D Models Are Not Included

The heart model files are excluded from this repository because of their large file size.

The following folder is ignored by Git:

Assets/Models/

To run the complete project, obtain the required model files separately and place them in:

Assets/Models/

After importing the model files, some Scene or Prefab references may need to be reassigned manually if Unity displays:

Missing Mesh
Missing Prefab
Missing Material

Recommended model formats:

.fbx
.obj
.stl

FBX is recommended for use in Unity.

## How to Open the Project

1. Clone the repository using GitHub Desktop or Git.

git clone https://github.com/Dolptd/heart-digital-twin.git

2. Open Unity Hub.

3. Select:

Add → Add project from disk

4. Select the project root folder containing:

Assets/
Packages/
ProjectSettings/

Do not select the Assets folder directly.

5. Install the required Unity Editor version if Unity Hub displays:

Missing Editor Version

6. Place the required heart model files inside:

Assets/Models/

7. Open the main Unity Scene:

Assets/Scenes/SampleScene.unity

8. Press Play to run the prototype.

## Model Preparation Workflow

### 1. Medical Image Input

The workflow begins with DICOM CT scan data.

### 2. Segmentation

The heart structures are segmented in 3D Slicer.

Possible tools include:

- Segment Editor
- TotalSegmentator
- SlicerHeart

### 3. 3D Reconstruction

The segmentation is converted into a closed surface model and exported as STL or OBJ.

### 4. Mesh Optimization

The model is processed in Blender for:

- Mesh cleanup
- Smoothing
- Removal of unnecessary geometry
- Polygon reduction
- Material preparation
- FBX export

### 5. Unity3D Visualization

The optimized model is imported into Unity3D and configured with scripts for interaction, transparency control, part selection, and heartbeat animation.

## Current Limitations

- The prototype does not use real-time patient data.
- The heartbeat animation is a simplified visual animation.
- The model has not been clinically validated.
- Anatomical accuracy depends on CT image quality and segmentation quality.
- The current system is intended for research and educational visualization only.

## Research Scope

This project focuses on creating an image-to-interaction workflow for heart anatomy visualization.

It does not currently include:

- Clinical diagnosis
- Treatment planning
- Blood flow simulation
- Computational fluid dynamics
- Electrophysiological simulation
- Biomechanical cardiac simulation
- Real-time synchronization with a physical patient

## Project Status

Current development status:

- [x] CT data preparation
- [x] Heart segmentation
- [x] 3D surface reconstruction
- [x] Blender mesh cleanup
- [x] Unity model import
- [x] Rotation
- [x] Zoom
- [x] Pan
- [x] Transparency control
- [x] BPM-based simplified heartbeat
- [x] Part selection
- [ ] Anatomical expert validation
- [ ] Multi-dataset evaluation
- [ ] Performance optimization
- [ ] User evaluation

## Authors

Patthadol Raksapram
Department of Smart Convergence System Engineering
Dong-A University

Saranrat Roteaim
Department of Smart Convergence System Engineering
Dong-A University

Advisor: Sangmun Shin
Department of Industrial Management Engineering
Dong-A University

## Academic Project

This repository supports the research project:

Development of a Heart Digital Twin Method by Using Unity3D

The project proposes a workflow for converting CT imaging data into an interactive Unity3D heart visualization prototype.

## License

This repository contains source code and Unity project configuration files.

Medical image data and 3D model files may be subject to separate dataset licenses and are not included in this repository.

Before reusing medical data or generated models, users must review the license and usage conditions of the original dataset.

## Contact

Patthadol Raksapram
Email: patthadol.raks@gmail.com
