using Microsoft.EntityFrameworkCore;
using TodoAPI.Models;

var builder = WebApplication.CreateBuilder(args);
// Webアプリケーションのビルダーを作成して、コマンドライン引数を受け取る。

builder.Services.AddControllers();
// コントローラーをサービスコンテナに追加する。これでMVCのコントローラーを使えるようになる。

builder.Services.AddDbContext<TodoContext>(opt =>
    opt.UseInMemoryDatabase("TodoList"));
// DbContextをサービスコンテナに追加し、InMemoryDatabaseを使うように設定する。これでTodoContextがInMemoryDatabaseを使用するようになる。

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
// エンドポイントのAPIエクスプローラーをサービスコンテナに追加する。これでAPIエンドポイントが自動で探索されるようになる。

builder.Services.AddSwaggerGen();
// Swaggerの生成サービスを追加する。これでAPIドキュメントを自動生成できる。

var app = builder.Build();
// 構築したWebアプリケーションをビルドする。

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
// 開発環境の場合、SwaggerとSwagger UIを使ってAPIドキュメントを提供する。

app.UseHttpsRedirection();
// HTTPリクエストをHTTPSにリダイレクトする。

app.UseAuthorization();
// 認可ミドルウェアを使用する。

app.MapControllers();
// コントローラーのルートマッピングを設定する。

app.Run();
