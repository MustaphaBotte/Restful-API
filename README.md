# 🌐 RESTful API — Learning Tracker

> Course: Introduction to RESTful APIs  
> Platform: Programming Advices  
> Goal: Understand how systems communicate and build real APIs

---

# 📘 Course Description

This course introduces the fundamentals of building and understanding RESTful APIs, focusing on how systems communicate over the web using HTTP.

It starts with the core concept of APIs, including the differences between XML and JSON, then moves into how web APIs work in real-world applications. The course explains REST principles such as stateless communication, resource-based design, and proper use of HTTP methods (GET, POST, PUT, DELETE) along with status codes.

Throughout the course, practical examples are used to demonstrate how to build a complete API, including CRUD operations. It also covers important software architecture concepts like Data Transfer Objects (DTO) and 3-Tier Architecture to help structure applications in a clean and scalable way.

Additionally, the course introduces handling files such as image uploads and retrieval, making it closer to real-world backend development scenarios.

By the end of this course, You will be able to design, build, and understand RESTful APIs, and see how they are used to connect web applications, mobile apps, and backend systems.

---

## 🎯 Course Objectives

- Understand what an API is and how it works
- Learn the fundamentals of HTTP communication
- Apply REST principles in API design
- Build a complete CRUD API
- Understand DTO and layered architecture
- Handle real-world scenarios like file uploads

---

## 🧠 Prerequisites

- Basic programming knowledge (preferably C#)
- Basic understanding of web concepts (optional)

---

## 🚀 Outcome

After completing this course, you will be able to:
- Build your own RESTful APIs
- Structure backend applications professionally
- Connect different systems through APIs

---

# 📌 00 - Overview

## 💡 What I Will Learn
- What APIs really are
- Difference between XML vs JSON
- HTTP fundamentals (requests / responses / status codes)
- RESTful principles
- Build real API (CRUD)
- Client ↔ Server communication
- DTO & 3-Tier architecture
- File handling (images)

---

# 🧠 01 - Introduction

## 📖 Topics
- What is API?
- XML vs JSON
- Real-world API examples

## 💡 Notes
- API = bridge between systems
- JSON is preferred (lighter, easier)
- APIs allow communication between:
  - Web apps
  - Mobile apps
  - Backend systems

## 🔥 When to use
- Any system needs to communicate with another system

---

# ⚙️ 02 - Win32 APIs (Concept Bridge)

## 📖 Topics
- What is Win32 API
- OS-level APIs examples

## 💡 Notes
- API is not only web
- OS also exposes APIs (like Windows functions)
- Same idea → different level

## 🧠 Insight
> Web APIs = evolution of system APIs

---

# 🌍 03 - Web APIs

## 📖 Topics
- What is Web API
- Benefits

## 💡 Notes
- Runs over HTTP
- Platform-independent
- Used everywhere (web, mobile, cloud)

## 🔥 Real Usage
- Frontend ↔ Backend
- Mobile apps ↔ Server

---

# 🔥 04 - RESTful API (CORE)

## 📖 Topics
- REST definition
- REST elements
- HTTP methods
- Status codes
- Request & Response

## 💡 Notes

### REST Principles
- Stateless
- Resource-based
- Uses HTTP

### HTTP Methods
- GET → Read
- POST → Create
- PUT → Update
- DELETE → Remove

### Status Codes
- 200 → OK
- 201 → Created
- 400 → Bad Request
- 404 → Not Found
- 500 → Server Error

## ⚠️ Pitfalls
- Mixing REST with RPC
- Bad endpoint naming

## 🧠 Insight
> REST = rules for clean communication between systems

---

# 🧪 05 - Setup & First API

## 📖 Topics
- Environment setup
- First Web API project

## 💡 Notes
- Server handles logic
- Client consumes API

---

# 🧑‍💻 06 - Student API Project (CRUD)

## 📖 Topics
- Get All
- Get By ID
- Get Passed / Avg
- Add (POST)
- Update (PUT)
- Delete (DELETE)

## 💡 Notes
- CRUD = core of APIs
- Each operation maps to HTTP method

## 🔥 Real Thinking
- API = interface to database
- Client never touches DB directly

---

# 🧱 07 - DTO (Data Transfer Object)

## 📖 Topics
- What is DTO
- Why use it

## 💡 Notes
- Separates internal model from external API
- Improves security & flexibility

---

# 🏗️ 08 - 3-Tier Architecture

## 📖 Topics
- API Layer
- Business Logic Layer
- Data Access Layer

## 💡 Notes
- Clean separation of concerns
- Easier maintenance
- Scalable structure

---

# 🖼️ 09 - File Handling

## 📖 Topics
- Upload images
- Retrieve images

## 💡 Notes
- APIs can handle files, not just JSON
- Used in real apps (profiles, uploads)

---

# 🧠 FINAL UNDERSTANDING

## 💡 Big Picture
- Systems are not isolated anymore
- APIs connect everything

## 🔥 What Changed in My Thinking
- From: “I build apps”
- To: “I connect systems”

## 🚀 Real-World Usage
- Web apps
- Mobile apps
- Microservices
- Cloud systems

---

# 📝 Problems / Practice

## Easy
- Call a public API (GET)

## Medium
- Build a CRUD API

## Advanced
- Add authentication
- Connect database
- Add validation

---

# 📊 Progress Tracker

- [x] Introduction
- [x] Win32 APIs
- [x] Web APIs
- [x] RESTful API
- [x] Setup & First API
- [x] CRUD Project
- [x] DTO
- [x] 3-Tier Architecture
- [x] File Handling
