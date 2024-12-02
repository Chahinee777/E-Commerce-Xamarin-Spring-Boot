package org.sid.ecommerce.Service;

import org.sid.ecommerce.Entities.Cart;
import org.sid.ecommerce.Entities.Order;
import org.sid.ecommerce.Entities.User;
import org.sid.ecommerce.Repository.CartRepository;
import org.sid.ecommerce.Repository.OrderRepository;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;

import java.util.Date;
import java.util.List;
import java.util.UUID;

@Service
public class OrderService {

    @Autowired
    private OrderRepository orderRepository;

    @Autowired
    private CartRepository cartRepository;

    @Autowired
    private UserService userService;

    public List<Order> getAllOrders() {
        return orderRepository.findAll();
    }

    public void deleteAllOrders() {
        orderRepository.deleteAll();
    }

    public void saveOrder(Long userId) {
        // Verify that the user exists
        User user = userService.getUserById(userId);
        if (user == null) {
            throw new IllegalArgumentException("User not found with ID: " + userId);
        }

        // Fetch the cart for the user
        List<Cart> carts = cartRepository.findByUserId(userId);
        if (carts == null || carts.isEmpty()) {
            throw new IllegalArgumentException("Cart is empty or not found for user with ID: " + userId);
        }

        // Create a new order
        Order order = new Order();
        order.setUser(user);
        order.setCommandeId(UUID.randomUUID().toString());
        order.setDateCommande(new Date());
        order.setStatue("Pending");
        order.setTypePayment("Credit Card"); // Example payment type, you can set this dynamically
        order.setAdresse("User Address"); // Example address, you can set this dynamically

        // Calculate total price and set product details
        int totalPrice = 0;
        for (Cart cart : carts) {
            totalPrice += cart.getProduct().getPrice() * cart.getQuantite();
            order.setProduct(cart.getProduct());
            order.setQuantite(cart.getQuantite());
            order.setPrix(cart.getProduct().getPrice());
        }
        order.setPrix(totalPrice);

        // Save the order
        orderRepository.save(order);

        // Clear the cart
        cartRepository.deleteAll(carts);
    }
}