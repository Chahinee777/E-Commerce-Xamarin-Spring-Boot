
package org.sid.ecommerce.Service;
import org.sid.ecommerce.Entities.Cart;

import java.util.List;

public interface CartService {
    Cart saveCart(Long idProduct, Long idUser);
    List<Cart> getCartByUser(Long idUser);
    Integer getCountCart(Long idUser);
    void updateCartQuantity(String sy, Long cid);
}

