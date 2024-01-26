# OurTime-Server
The OurTime Server project is a comprehensive backend service built using ASP.NET Core. It provides user authentication and management, as well as real-time communication through SignalR for web-based applications. The project includes features for user registration, login, updating user information, and deleting user accounts. Additionally, it incorporates a SignalR hub (WebRTC) for handling real-time communication tasks such as initiating and accepting calls, as well as sending messages.

### Client Repo Link:
https://github.com/quyson/ourtime-client

## Application Structure
The configuration for the OurTime Server project involves setting up CORS (Cross-Origin Resource Sharing) policies to allow communication with specific origins, configuring SignalR, adding authentication middleware for JWT (JSON Web Token), and establishing a database connection using Entity Framework Core.

### User Services
1. UserController:
The UserController is a RESTful API controller responsible for handling user-related HTTP requests. It includes endpoints for retrieving the authenticated user's name, registering a new user, logging in, updating user information, and deleting user accounts.
2. UserService:
The UserService class implements the IUserService interface, providing functionalities for user operations. It includes methods for user registration, login, updating user information, and deleting user accounts. The CreateToken method generates a JWT token for user authentication.
3. User and UserDTO Models:
The User class represents the entity model for a user, including properties like Id, Username, Email, PasswordHash, FirstName, and LastName. The UserDto class is a data transfer object used for user registration and login.
#### JWT Middleware in Program.cs file
builder.Services.AddAuthentication().AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        ValidateAudience = false,
        ValidateIssuer = false,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration.GetSection("AppSettings:Token").Value!))
    };
});

### Database Connection for MSSQL (Entity Framework Core)
The DataContext class is the database context defined using Entity Framework Core. It includes a Users DbSet for interacting with the User entity. Entity Framework Core migrations are included for creating the initial database schema.
#### Database Context Injection in Program.cs file
builder.Services.AddDbContext<DataContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

### SignalR Connection Hub (webRTC.cs)
The WebRTC class is a SignalR hub in the OurTime server project, designed to facilitate real-time communication between clients. It utilizes the SignalR library to manage WebSocket connections and enable seamless, bidirectional communication. This hub contains methods that collectively enable real-time communication features such as initiating and accepting calls, as well as broadcasting messages to all connected clients. The Console.WriteLine statements are used for logging relevant events during the communication process.

In the context of the OurTime server project, this hub plays a crucial role in supporting the real-time communication needs of the application. It can be extended and customized to fit specific requirements for real-time interactions.

Below is a summary of the key functionalities provided by this hub:
1. Connection Handling:
The OnConnectedAsync method is invoked when a new client successfully connects to the hub. It logs the unique identifier (ConnectionId) of the newly connected client.
2. CallUser Method:
The CallUser method is responsible for initiating a call to a specific user. It sends a SignalR message (IncomingCall) to the targeted user with relevant signaling data, including the sender's identifier (from) and additional call details.
3. AcceptCaller Method:
The AcceptCaller method is used to accept an incoming call. It sends a SignalR message (CallAccepted) to the caller, including the accepted call data.
4. SendMessage Method:
The SendMessage method broadcasts a message to all connected clients. It sends a SignalR message (NewMessage) with the sender's username and the message content.

## Additional Notes
The server is configured to allow connections from the specified client origin using CORS.
MSSQL is employed for storing user information.
Bcrypt.NET is used to encrypt sensitive user information such as passwords


   
   
