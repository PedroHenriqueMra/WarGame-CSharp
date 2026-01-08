public record HandShakeEnvelope<TPayLoad>
(
    string Type,
    TPayLoad Payload    
);
