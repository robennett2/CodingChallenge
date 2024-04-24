namespace PizzaPlace.Services.Infrastructure.Exceptions;

public class OrderNotFoundException(Guid id) : Exception($"Order with id {id} was not found");