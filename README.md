# Web News Website Project

---

I developed an online News Project as a fullstack developer in a very limited time for my "Software Development" lecture. I used various technologies, libraries, and approaches such as "N-Tier Architecture, CKEditor, TinyMCE, Datatables, LinQ, SQLite, Bootstrap" during the development. After a quick introduction, let's dive into it. In this project, there are some rules that the project has to contain.

These are:

## 1. Identity System (Authentication / Authorization)

### 1.1 Roles:

- Admin
- Director
- Author

### 1.2 User Types

- **Subscriber**: Registered user.
- **Guest**: Regular user who is not registered.

### 1.3 Post Types

There are subscriber-only posts that only registered and logged-in users can see.

This project also has an admin panel that allows users with the Admin or Director role to manage their department and perform various actions such as:

- Delete, approve, or reject posts or users,
- Display logs about deleting and approval activities,
- Display monthly statistics,
- Display the structure of departments,

This project also includes many more authorization logics inside. I will share a couple of photos from this project below. Just take a look at how it looks. If you want to explore this app by yourself, you can use "adminuser@test.com" and "Password123" credentials to log in. Alternatively, you can open the DB via DB Browser or similar programs to access the database.

The project structure is based on MVC, and it uses WYSIWYG tools like CKEditor, front-end libraries like Bootstrap. There are three main roles defined in the project: Writer (Author), Director, and Admin. SQLite was used as the database to make it easy for everyone to use during development. General user registrations require admin approval. The news on the website is categorized into two main types: those visible to registered users and those visible to guests (non-registered users).

## Roles

- Admin
- Director
- Writer (Author)

## Homepage

![Homepage](https://github.com/byrmTelli/NewsApp.MVC/blob/master/uygulamaG%C3%B6rselleri/homepage.png?raw=true)

## Login and Register Pages

![Login Page](https://github.com/byrmTelli/NewsApp.MVC/blob/master/uygulamaG%C3%B6rselleri/loginPage.png?raw=true)
![Register Page](https://github.com/byrmTelli/NewsApp.MVC/blob/master/uygulamaG%C3%B6rselleri/registerPage.png?raw=true)

## View as Logged In User

![Logged In User View](https://github.com/byrmTelli/NewsApp.MVC/blob/master/uygulamaG%C3%B6rselleri/loggedInUserView.png?raw=true)

## Post Detail Page

![Post Detail Page](https://github.com/byrmTelli/NewsApp.MVC/blob/master/uygulamaG%C3%B6rselleri/detailPage.png?raw=true)

## Admin Panel Dashboard

### Dashboard

![Dashboard](https://github.com/byrmTelli/NewsApp.MVC/blob/master/uygulamaG%C3%B6rselleri/dashboard.png?raw=true)

### Logs

![Logs](https://github.com/byrmTelli/NewsApp.MVC/blob/master/uygulamaG%C3%B6rselleri/userApprovalLogs.png?raw=true)

## Management Panels

### User Management

![User Management](https://github.com/byrmTelli/NewsApp.MVC/blob/master/uygulamaG%C3%B6rselleri/userManagement.png?raw=true)

### Role Management

![Role Management](https://github.com/byrmTelli/NewsApp.MVC/blob/master/uygulamaG%C3%B6rselleri/roleManagement.png?raw=true)

## Update Post Page

![Update Post Page](https://github.com/byrmTelli/NewsApp.MVC/blob/master/uygulamaG%C3%B6rselleri/postUpdate.png?raw=true)
