package org.sid.ecommerce.Web;

import org.sid.ecommerce.Entities.Order;
import org.sid.ecommerce.Service.OrderService;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.*;

import java.util.List;

@RestController
@RequestMapping("/api/orders")
@CrossOrigin(origins = {"http://192.168.100.220", "http://localhost"})
public class OrderController {

    @Autowired
    private OrderService orderService;

    @GetMapping("/afficheTous")
    public ResponseEntity<List<Order>> getAllOrders() {
        List<Order> orders = orderService.getAllOrders();
        return ResponseEntity.ok(orders);
    }

    @DeleteMapping("/deleteAll")
    public ResponseEntity<?> deleteAllOrders() {
        try {
            orderService.deleteAllOrders();
            return ResponseEntity.ok("All orders deleted successfully");
        } catch (Exception e) {
            return ResponseEntity.status(500).body("Failed to delete orders: " + e.getMessage());
        }
    }
}