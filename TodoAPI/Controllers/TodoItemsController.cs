using System; // 基本的な型や関数を提供する名前空間
using System.Collections.Generic; // ジェネリックコレクションを提供する名前空間
using System.Linq; // クエリ操作を提供する名前空間
using System.Threading.Tasks; // 非同期プログラミングをサポートする名前空間
using Microsoft.AspNetCore.Http; // HTTPリクエストとレスポンスを提供する名前空間
using Microsoft.AspNetCore.Mvc; // MVC機能を提供する名前空間
using Microsoft.EntityFrameworkCore; // Entity Framework Coreの機能を提供する名前空間
using TodoAPI.Models; // TodoAPIのモデルを提供する名前空間

namespace TodoAPI.Controllers // コントローラーを含む名前空間
{
    [Route("api/[controller]")] // ルートを設定。APIのエンドポイントはコントローラー名に基づく
    [ApiController] // このクラスはAPIコントローラーであることを示す
    public class TodoItemsController : ControllerBase // ControllerBaseを継承
    {
        private readonly TodoContext _context; // データベースコンテキストのインスタンス

        public TodoItemsController(TodoContext context) // コンストラクタ。DIでTodoContextを受け取る
        {
            _context = context; // コンテキストをフィールドに設定
        }

        // GET: api/TodoItems
        [HttpGet] // HTTP GETリクエストを処理
        public async Task<ActionResult<IEnumerable<TodoItem>>> GetTodoItems()
        {
            return await _context.TodoItems.ToListAsync(); // TodoItemsテーブルの全データを取得
        }

        // GET: api/TodoItems/5
        [HttpGet("{id}")] // HTTP GETリクエストを処理。IDをパラメータとして受け取る
        public async Task<ActionResult<TodoItem>> GetTodoItem(long id)
        {
            var todoItem = await _context.TodoItems.FindAsync(id); // 指定IDのデータを取得
            if (todoItem == null) // データが見つからない場合
            {
                return NotFound(); // 404 Not Foundを返す
            }
            return todoItem; // データを返す
        }

        // PUT: api/TodoItems/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")] // HTTP PUTリクエストを処理。IDをパラメータとして受け取る
        public async Task<IActionResult> PutTodoItem(long id, TodoItem todoItem)
        {
            if (id != todoItem.id) // パスIDとデータIDが一致しない場合
            {
                return BadRequest(); // 400 Bad Requestを返す
            }

            _context.Entry(todoItem).State = EntityState.Modified; // データを変更状態に設定

            try
            {
                await _context.SaveChangesAsync(); // 変更を保存
            }
            catch (DbUpdateConcurrencyException) // 同時更新エラーの場合
            {
                if (!TodoItemExists(id)) // データが存在しない場合
                {
                    return NotFound(); // 404 Not Foundを返す
                }
                else
                {
                    throw; // 他の例外は再スロー
                }
            }

            return NoContent(); // 204 No Contentを返す
        }

        // POST: api/TodoItems
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost] // HTTP POSTリクエストを処理
        public async Task<ActionResult<TodoItem>> PostTodoItem(TodoItem todoItem)
        {
            _context.TodoItems.Add(todoItem); // データを追加
            await _context.SaveChangesAsync(); // 変更を保存

            return CreatedAtAction(nameof(GetTodoItem), new { id = todoItem.id }, todoItem); // 201 Createdを返し、新しいデータの情報を含む
        }

        // DELETE: api/TodoItems/5
        [HttpDelete("{id}")] // HTTP DELETEリクエストを処理。IDをパラメータとして受け取る
        public async Task<IActionResult> DeleteTodoItem(long id)
        {
            var todoItem = await _context.TodoItems.FindAsync(id); // 指定IDのデータを取得
            if (todoItem == null) // データが見つからない場合
            {
                return NotFound(); // 404 Not Foundを返す
            }

            _context.TodoItems.Remove(todoItem); // データを削除
            await _context.SaveChangesAsync(); // 変更を保存

            return NoContent(); // 204 No Contentを返す
        }

        private bool TodoItemExists(long id) // 指定IDのデータが存在するか確認
        {
            return _context.TodoItems.Any(e => e.id == id); // 存在する場合はtrueを返す
        }
    }
}
