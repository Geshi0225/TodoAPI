using Microsoft.EntityFrameworkCore;
using TodoAPI.Models;

var builder = WebApplication.CreateBuilder(args);
// Web�A�v���P�[�V�����̃r���_�[���쐬���āA�R�}���h���C���������󂯎��B

builder.Services.AddControllers();
// �R���g���[���[���T�[�r�X�R���e�i�ɒǉ�����B�����MVC�̃R���g���[���[���g����悤�ɂȂ�B

builder.Services.AddDbContext<TodoContext>(opt =>
    opt.UseInMemoryDatabase("TodoList"));
// DbContext���T�[�r�X�R���e�i�ɒǉ����AInMemoryDatabase���g���悤�ɐݒ肷��B�����TodoContext��InMemoryDatabase���g�p����悤�ɂȂ�B

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
// �G���h�|�C���g��API�G�N�X�v���[���[���T�[�r�X�R���e�i�ɒǉ�����B�����API�G���h�|�C���g�������ŒT�������悤�ɂȂ�B

builder.Services.AddSwaggerGen();
// Swagger�̐����T�[�r�X��ǉ�����B�����API�h�L�������g�����������ł���B

var app = builder.Build();
// �\�z����Web�A�v���P�[�V�������r���h����B

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
// �J�����̏ꍇ�ASwagger��Swagger UI���g����API�h�L�������g��񋟂���B

app.UseHttpsRedirection();
// HTTP���N�G�X�g��HTTPS�Ƀ��_�C���N�g����B

app.UseAuthorization();
// �F�~�h���E�F�A���g�p����B

app.MapControllers();
// �R���g���[���[�̃��[�g�}�b�s���O��ݒ肷��B

app.Run();
