using System;
using System.Net;
using System.Net.WebSockets;
using System.Text;
using csgame_backend.GameEnvironment_websocket;
using csgame_backend.Patterns;
using csgame_backend.player_websocket;
using WebSocketSharp.Server;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

WebSocketServer wssv = new WebSocketServer("ws://127.0.0.1:8888");
wssv.AddWebSocketService<Game>("/Game");
wssv.AddWebSocketService<Projectile>("/Projectile");
wssv.AddWebSocketService<GameEnvironment>("/GameEnvironment");
wssv.Start();


app.Run();
