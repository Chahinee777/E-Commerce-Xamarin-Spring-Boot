package org.sid.ecommerce.Web;

import org.sid.ecommerce.Entities.Cart;
import org.sid.ecommerce.Entities.User;
import org.sid.ecommerce.Service.CartService;
import org.sid.ecommerce.Service.OrderService;
import org.sid.ecommerce.Service.UserService;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.http.ResponseEntity;
import org.springframework.util.ObjectUtils;
import org.springframework.web.bind.annotation.*;

import jakarta.servlet.http.HttpSession;

import java.security.Principal;
import java.util.List;

@RestController
@RequestMapping("/api/users")
@CrossOrigin(origins = {"http://192.168.100.220", "http://localhost"})
public class UserController {

    @Autowired
    private UserService userService;

    @Autowired
    private CartService cartService;

    @Autowired
    private OrderService orderService;

    @PostMapping("/register")
    public ResponseEntity<?> register(@RequestBody User user) {
        try {
            System.out.println("Registering user: " + user.getEmail()); // Log the user email
            User newUser = userService.registerUser(user);
            System.out.println("User registered successfully: " + newUser.getEmail()); // Log success
            return ResponseEntity.ok(newUser);
        } catch (Exception e) {
            e.printStackTrace(); // Log the exception
            return ResponseEntity.status(500).body("Registration failed: " + e.getMessage());
        }
    }

    @PostMapping("/login")
    public ResponseEntity<?> login(@RequestBody User user) {
        User loggedInUser = userService.login(user.getEmail(), user.getPassword());
        if (loggedInUser != null) {
            return ResponseEntity.ok(loggedInUser);
        } else {
            return ResponseEntity.status(401).body("Invalid email or password");
        }
    }

    @PostMapping("/addCart")
    public String addToCart(@RequestParam Long productId, @RequestParam Long userId, HttpSession session) {
        Cart saveCart = cartService.saveCart(productId, userId);
        if (ObjectUtils.isEmpty(saveCart)) {
            session.setAttribute("errorMsg", "Product not added to cart");
        } else {
            session.setAttribute("successMsg", "Product added to cart");
        }
        return "redirect:/login";
    }

    @GetMapping("/{userId}/cart")
    public ResponseEntity<?> getCart(@PathVariable Long userId) {
        List<Cart> carts = cartService.getCartByUser(userId);
        if (ObjectUtils.isEmpty(carts)) {
            return ResponseEntity.status(404).body("No items found in the cart");
        }
        return ResponseEntity.ok(carts);
    }

    @GetMapping("/updateCartQuantity")
    public String updateCartQuantity(@RequestParam String sy, @RequestParam Long cartId){
        cartService.updateCartQuantity(sy, cartId);
        return "redirect:/api/users/cart";
    }

    private User getLoggedInUserDetails(Principal p) {
        String email = p.getName();
        return userService.getUserByEmail(email);
    }

    @GetMapping("/orders")
    public String orderPage(){
        return "/user/order";
    }

    @PostMapping("/save-order")
    public ResponseEntity<?> saveOrder(@RequestParam Long userId) {
        try {
            orderService.saveOrder(userId);
            return ResponseEntity.ok("Order saved successfully");
        } catch (Exception e) {
            e.printStackTrace(); // Log the exception
            return ResponseEntity.status(500).body("Order saving failed: " + e.getMessage());
        }
    }
}