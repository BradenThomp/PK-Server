﻿using Application.Features.Speaker.Dtos;
using System;

namespace Application.Features.Tracking.Dtos
{
    public record TrackerDto(string MacAddress, double Longitude, double Latitude, DateTime Updated, SpeakerDto AssignedSpeaker);
}
