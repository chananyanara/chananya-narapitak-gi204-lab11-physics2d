using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float Speed; // ความเร็วในการเดิน
    public float JumpForce; // แรงในการกระโดด
    public bool isJumping; // เช็คว่ากำลังกระโดดอยู่หรือไม่

    float move;
    Rigidbody2D rb2d;

    void Start()
    {
        // ดึง Component Rigidbody2D มาใช้งาน
        rb2d = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // 1. การเคลื่อนที่ ซ้าย-ขวา
        move = Input.GetAxis("Horizontal");
        rb2d.linearVelocity = new Vector2(move * Speed, rb2d.linearVelocity.y);

        // 2. การกระโดด (จะกระโดดได้ต่อเมื่อกดปุ่ม Jump และ ไม่ได้กำลังกระโดดอยู่)
        if (Input.GetButtonDown("Jump") && !isJumping)
        {
            rb2d.AddForce(new Vector2(rb2d.linearVelocity.x, JumpForce));
            Debug.Log("Jump"); // แสดงข้อความใน Console เพื่อเช็คการทำงาน
        }
    }

    // 3. เช็คว่าตัวละครแตะพื้นหรือยัง (Collision Detection)
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            isJumping = false; // ถ้าแตะพื้นที่มี Tag ว่า Ground ให้เลิกกระโดด
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            isJumping = true; // ถ้าออกจากพื้น ให้ถือว่ากำลังลอยอยู่ (กระโดด)
        }
    }
}