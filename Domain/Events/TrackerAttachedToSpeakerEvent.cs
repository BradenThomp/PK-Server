using Domain.Common.Events;
using Domain.Models;
using System;

namespace Domain.Events
{
    [Serializable]
    public record TrackerAttachedToSpeakerEvent(Speaker AssignedSpeaker) : IEvent;
}
