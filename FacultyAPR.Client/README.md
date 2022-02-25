# FacultyAPR.Client
The front end is written with Node.js, TypeScript and ReactJS. If you are unfamiliar with Node.js or ReactJS or seek advice on deployment, see CONTRIBUTING.md for further guidance.

We have implemented most of the business logic for our project in the front end. This includes verifying that the form is complete, the reviewer workflow, ensuring user authorization and more.

## src/
This folder contains all the components for our application. Notable files include:
- FacultyAPR.Client\src\common\helpers\form.tsx
  - this file is full of helper functions used in the dev phase of the project.
- .env.development and .env.production
  - environment files to point to url server

The folder layout is as follows:

### api
Contains the routing information to communicate with the backend.

### common
Some common types and the ever important helpers\form.tsx.

### components
Folder containing all the React components used in the project.

### js
Constants written in plain JavaScript.

### models
Re-implementation of the models found in lib\FacultyAPR.Models\FacultyAPR.Models for the front end.

### redux
Files pertaining to the redux module.