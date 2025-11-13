using UnityEngine;
using UnityEngine.InputSystem; //記得引用InputSystem命名空間

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f; //移動速度
    [SerializeField] private float jumpForce = 10f; //跳躍力
    [SerializeField] private LayerMask groundLayer; //地面圖層，用於檢查是否在地面上
    private float moveInputX; //儲存移動輸入，X軸左右
    private Rigidbody2D rb; //角色的Rigidbody2D元件
    private bool isGrounded; //是否在地面上
    private Collider2D coll; //角色的Collider2D元件
    int jumpCount = 0; //計算跳躍次數
    private void Awake() //在遊戲開始前執行，比Start還早
    {
        rb = GetComponent<Rigidbody2D>(); //取得Rigidbody2D元件
        coll = GetComponent<Collider2D>(); //取得Collider2D元件
        jumpCount = 0;
    }
    public void OnMove(InputValue value) //移動輸入事件，參數是輸入事件的內容
    {
        moveInputX = value.Get<float>(); //取得X軸的輸入值，-1到1之間
    }
    public void OnJump(InputValue value) //跳躍輸入事件
    {
        if (value.isPressed && (isGrounded || jumpCount < 2)) //當按下跳躍鍵時，按鈕的狀態為isPressed
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce); //給予Y軸跳躍力
            Invoke("AddJumpCount", 0.1f);
            
        }
    }
    private void FixedUpdate() //物理更新函式
    {
        //根據輸入移動角色
        rb.linearVelocity = new Vector2(moveInputX * moveSpeed, rb.linearVelocity.y);
        //發射一條向下的射線，從角色的Collider中心開始，長度為Collider的Y軸半徑加上一點點偏移量，回傳碰撞資訊
        RaycastHit2D hit = Physics2D.Raycast(coll.bounds.center, Vector2.down, coll.bounds.extents.y + 0.1f, groundLayer);
        Debug.DrawRay(coll.bounds.center, Vector2.down * (coll.bounds.extents.y + 0.1f), Color.red); //繪製射線，方便除錯
        isGrounded = hit.collider != null; //如果射線碰到地面圖層，表示在地面上
        if (isGrounded)
        {
            jumpCount = 0; //重置跳躍次數
        }
    }

    void AddJumpCount()
    {
        jumpCount++;
        Debug.Log("Jump Count: " + jumpCount);
    }
}