# Events

<p align="center">
  <img src="https://socialify.git.ci/renyuan-fei/Events/image?language=1&amp;name=1&amp;owner=1&amp;theme=Light" alt="project-image">
</p>

Events is a social networking website where users can engage in activities by creating and joining events. Within these events users can participate in group chats and follow others to receive real-time updates. Additionally users can share their life experiences by uploading photos.

## 🚀 Demo

[https://eventsharbor.club](https://eventsharbor.club)

## 🧐 Features

Here're some of the project's best features:

- Users can log in register and log out.
- Create modify cancel and reactivate activities they've created.
- Join activities created by others.
- Engage in real-time chat within activities
- Edit their own profile.
- Upload photos for their profile as well as for activities they've created.
- Filter activities by date category participation status and host and search by title.
- Receive real-time notifications for the following events:
  - New followers.
  - Activity creations.
  - Modifications or cancellations of activities they've joined.
  - Modifications or cancellations of activities they've created.

## 💻 Built with

**Client:** React, Redux, MUI,React Query, typescript

**Server:** ASP .NET Core, .NET 6, PostgreSQL,EF Core

**CI/CD** GitHub Action

## Prerequisites

### Client:

| Package           | Version  |
|-------------------|----------|
| React             | ^17.0.0  || ^18.0.0 |
| React-DOM         | ^17.0.0  || ^18.0.0 |
| Axios             | ^1.6.2   |
| Date-fns          | ^2.30.0  |
| Dayjs             | ^1.11.10 |
| Lodash            | ^4.17.21 |
| React-Dropzone    | ^14.2.3  |
| React-Hook-Form   | ^7.49.3  |
| React-Html-Parser | ^2.0.2   |
| React-Query       | ^3.39.3  |
| React-Quill       | ^2.0.0   |
| React-Redux       | ^8.1.3   |
| React-Router      | ^6.19.0  |
| React-Router-DOM  | ^6.19.0  |
| Redux             | ^4.2.1   |
| Redux-Thunk       | ^2.4.2   |
| Styled-Components | ^6.1.1   |
| Zod               | ^3.22.4  |

### Server:

| Dependencies                                         | version |
|------------------------------------------------------|---------|
| AutoMapper                                           | 10.1.1  |
| AutoMapper.Extensions.Microsoft.DependencyInjection  | 8.1.1   |
| Ardalis.GuardClauses                                 | 4.3.0   |
| CloudinaryDotNet                                     | 1.24.0  |
| Microsoft.AspNetCore.ApiAuthorization.IdentityServer | 6.0.25  |
| Microsoft.EntityFrameworkCore                        | 6.0.25  |
| Microsoft.EntityFrameworkCore.Design                 | 6.0.25  |
| Microsoft.EntityFrameworkCore.SqlServer              | 6.0.25  |
| MediatR                                              | 12.2.0  |
| Npgsql.EntityFrameworkCore.PostgreSQL                | 6.0.22  |
| Serilog.AspNetCore                                   | 8.0.0   |
| Serilog.Sinks.Console                                | 5.0.0   |
| Serilog.Sinks.File                                   | 5.0.0   |
| Swashbuckle.AspNetCore                               | 6.5.0   |

## 🛠️ Installation Steps:

```sh
git clone https://github.com/renyuan-fei/Events.git
```
create .env variable for Docker(if you want
upload your image,please run follow bash script,
it will let your fill cloudinary apikey and create a .env file)

Obtain Your Cloudinary API Key:
If you haven't already, register for an account and generate an API key on 
[Cloudinary](https://cloudinary.com).

* Windows
```shell
bash init.sh
```

* Linux or MacOS
``` sh
chmod +x init.sh
./init.sh
```

To start Docker containers for the project, please use:
The `up` command is used to start the services defined in the `docker-compose.yml` file, in detached mode (`-d`).
``` sh
docker compose up -d
```

To stop Docker containers:
The `down` command is used to stop and remove containers.
``` sh
docker compose down
```

View website
```http request
https://localhost:7095
```
<h2>Project Screenshots:</h2>

**Main page:**
<br>
<img src="https://renyuan-fei.github.io/Media/Events/Capture-2024-03-26-145349.png" alt="project-screenshot" width="320">

**Login and Register**
<br>
<img src="https://renyuan-fei.github.io/Media/Events/Capture-2024-03-26-145724.png" alt="project-screenshot" width="320">
<br>
<img src="https://renyuan-fei.github.io/Media/Events/Capture-2024-03-26-145814.png" alt="project-screenshot" width="320">
<br>
<img src="https://renyuan-fei.github.io/Media/Events/Capture-2024-03-26-145919.png" alt="project-screenshot" width="320">

**Activity list:**
<br>
<img src="https://renyuan-fei.github.io/Media/Events/Capture-2024-03-26-150226.png" alt="project-screenshot" width="320">
<br>
<img src="https://renyuan-fei.github.io/Media/Events/Capture-2024-03-26-150252.png" alt="project-screenshot" width="320">

**Notification:**
<br>
<img src="https://renyuan-fei.github.io/Media/Events/Capture-2024-03-26-150344.png" alt="project-screenshot" width="320">

**User profile:**
<br>
<img src="https://renyuan-fei.github.io/Media/Events/Capture-2024-03-26-150406.png" alt="project-screenshot" width="320">
<br>
<img src="https://renyuan-fei.github.io/Media/Events/Capture-2024-03-26-151613.png" alt="project-screenshot" width="320">

**Photo Upload**
<br>
<img src="https://renyuan-fei.github.io/Media/Events/Capture-2024-03-26-150437.png" alt="project-screenshot" width="320">
<br>
<img src="https://renyuan-fei.github.io/Media/Events/Capture-2024-03-26-150709.png" alt="project-screenshot" width="320">
<br>
<img src="https://renyuan-fei.github.io/Media/Events/Capture-2024-03-26-151659.png" alt="project-screenshot" width="320">

**Activity**
<br>
<img src="https://renyuan-fei.github.io/Media/Events/Capture-2024-03-26-151756.png" alt="project-screenshot" width="320">
<br>
<img src="https://renyuan-fei.github.io/Media/Events/Capture-2024-03-26-151716.png" alt="project-screenshot" width="320">
<br>
<img src="https://renyuan-fei.github.io/Media/Events/Capture-2024-03-26-171128.png" alt="project-screenshot" width="320">
<br>
<img src="https://renyuan-fei.github.io/Media/Events/Capture-2024-03-26-171404.png" alt="project-screenshot" width="320">
<br>
<img src="https://renyuan-fei.github.io/Media/Events/Capture-2024-03-26-171417.png" alt="project-screenshot" width="320">

**Follow List**
<br>
<img src="https://renyuan-fei.github.io/Media/Events/Capture-2024-03-26-151945.png" alt="project-screenshot" width="320">

