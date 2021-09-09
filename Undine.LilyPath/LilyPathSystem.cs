using LilyPath;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using Undine.Core;
using Undine.MonoGame;

namespace Undine.LilyPath
{
    public class LilyPathSystem : UnifiedSystem<LilyPathComponent, TransformComponent>
    {
        private Dictionary<LilyPathDrawType, Action<LilyPathComponent, TransformComponent>> _lilyPathDrawType;
        public DrawBatch _drawBatch;

        public LilyPathSystem(GraphicsDevice graphicsDevice)
        {
            _lilyPathDrawType = new Dictionary<LilyPathDrawType, Action<LilyPathComponent, TransformComponent>>();
            _drawBatch = new DrawBatch(graphicsDevice);
            _lilyPathDrawType.Add(LilyPathDrawType.FillCircle, FillCircle);
            _lilyPathDrawType.Add(LilyPathDrawType.FillRectangleCentered, FillRectangleCentered);
        }

        public override void PreProcess()
        {
            _drawBatch.Begin(DrawSortMode.Deferred);
        }
        public override void PostProcess()
        {
            _drawBatch.End();
        }
        public override void ProcessSingleEntity(int entityId,
            ref LilyPathComponent a, ref TransformComponent b)
        {
            _lilyPathDrawType[a.LilyPathDrawType](a, b);
        }

        private void FillCircle(LilyPathComponent arg1, TransformComponent arg2)
        {
            _drawBatch.FillCircle(arg1.Brush, arg2.Position, arg1.Width);
        }

        private void FillRectangleCentered(LilyPathComponent arg1, TransformComponent arg2)
        {
            _drawBatch.FillRectangleCentered(arg1.Brush, arg2.Position, arg1.Width, arg1.Height, arg2.Rotation);
        }
    }
}