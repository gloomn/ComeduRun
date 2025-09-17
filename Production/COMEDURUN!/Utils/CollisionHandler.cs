//Copyright (C) 2025 Lee Ki Joon
using System.Windows.Forms;

namespace COMEDURUN_.Utils
{
    public static class CollisionHandler
    {
        public static bool isColliding(PictureBox A, PictureBox B)
        {
            return A.Bounds.IntersectsWith(B.Bounds);
        }
    }
}
