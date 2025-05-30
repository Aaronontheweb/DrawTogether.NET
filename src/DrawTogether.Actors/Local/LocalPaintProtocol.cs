﻿using DrawTogether.Entities;
using DrawTogether.Entities.Drawings;
using DrawTogether.Entities.Drawings.Messages;
using DrawTogether.Entities.Users;

namespace DrawTogether.Actors.Local;

public static class LocalPaintProtocol
{
    public interface IPaintSessionMessage : IDrawingSessionEvent
    {
        /// <summary>
        /// User who did the thing
        /// </summary>
       UserId UserId { get; } 
    }
    
    public sealed class ClearDrawingSession(DrawingSessionId drawingSessionId, UserId userId) : IPaintSessionMessage
    {
        public UserId UserId { get; } = userId;
        public DrawingSessionId DrawingSessionId { get; } = drawingSessionId;
    }
    
    public sealed class JoinPaintSession(DrawingSessionId drawingSessionId, UserId userId) : IPaintSessionMessage
    {
        public UserId UserId { get; } = userId;
        public DrawingSessionId DrawingSessionId { get; } = drawingSessionId;
    }
    
    public sealed class LeavePaintSession(DrawingSessionId drawingSessionId, UserId userId) : IPaintSessionMessage
    {
        public UserId UserId { get; } = userId;
        public DrawingSessionId DrawingSessionId { get; } = drawingSessionId;
    }

    /// <summary>
    /// Sent when a point is added to a continuous stroke (mouse/touch movement)
    /// </summary>
    public sealed class AddPointToConnectedStroke(
        Point point,
        DrawingSessionId drawingSessionId,
        UserId userId, GreaterThanZeroInteger strokeWidth, Color strokeColor)
        : IPaintSessionMessage
    {
        public Point Point { get; } = point;
        
        public DrawingSessionId DrawingSessionId { get; } = drawingSessionId;
        public UserId UserId { get; } = userId;
        
        public GreaterThanZeroInteger StrokeWidth { get; } = strokeWidth;
    
        public Color StrokeColor { get; } = strokeColor;
    }
    
    /// <summary>
    /// Sent when the user releases the mouse/touch, indicating the current stroke is complete
    /// </summary>
    public sealed class StrokeCompleted(
        DrawingSessionId drawingSessionId,
        UserId userId)
        : IPaintSessionMessage
    {
        public DrawingSessionId DrawingSessionId { get; } = drawingSessionId;
        public UserId UserId { get; } = userId;
    }
}