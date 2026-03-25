# Harmony 🎵

A Blazor WebAssembly application that matches users based on their music taste by consuming SoundMatchAPI.

## Overview

Harmony is a social music discovery platform that connects people through their shared love of music. By analyzing your Spotify listening habits, Harmony finds other users with similar music tastes and calculates compatibility scores based on mutual artists, songs, and genres.

## Features

- **Spotify Integration** - Connect your Spotify account to import your music preferences
- **Music Matching** - Find users with similar music taste based on:
  - Favorite songs
  - Favorite artists
  - Favorite genres
- **Compatibility Scores** - See how compatible you are with other users
- **User Profiles** - View detailed profiles showing top artists, songs, and genres
- **Authentication** - Secure user registration and login with JWT tokens

## Tech Stack

- **Frontend**: Blazor WebAssembly (.NET 8)
- **Authentication**: JWT-based authentication with ASP.NET Core Identity
- **Storage**: Blazored.LocalStorage for client-side token storage
- **Object Mapping**: AutoMapper
- **API Client**: NSwag-generated HTTP client
- **Styling**: Bootstrap 5, Bootstrap Icons, Font Awesome



## Getting Started

### Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- Visual Studio 2022 or VS Code
- A running instance of the Harmony API backend (expected at `https://localhost:7165`)
- NUGET packages:
  - Blazored.LocalStorage
  - AutoMapper
  - Microsoft.AspNetCore.Authorization
  - Microsoft.AspNetCore.Components.Authorization
  - Microsoft.AspNetCore.WebUtilities
  - Newtonsoft.Json
  - System.IdentityModel.Tokens.Jwt
- A tunnel like ngrok might be needed to access Spotify API

### Running the Application

1. Clone the repository
2. Restore dependencies
3. Run the application (ensure API is running as well)
4. Open browser at displayed URL
5. Enjoy!

