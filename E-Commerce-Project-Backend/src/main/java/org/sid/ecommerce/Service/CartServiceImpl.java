package org.sid.ecommerce.Service;

import org.sid.ecommerce.Entities.Cart;
import org.sid.ecommerce.Entities.Product;
import org.sid.ecommerce.Entities.User;
import org.sid.ecommerce.Repository.CartRepository;
import org.sid.ecommerce.Repository.UserRepository;
import org.sid.ecommerce.Repository.ProductRepository;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;
import org.springframework.util.ObjectUtils;

import java.util.ArrayList;
import java.util.List;

@Service
public class CartServiceImpl implements CartService {

    @Autowired
    private CartRepository cartRepository;
    @Autowired
    private UserRepository userRepository;
    @Autowired
    private ProductRepository productRepository;

    @Override
    public Cart saveCart(Long productId, Long userId) {
        User user = userRepository.findById(userId).get();
        Product product = productRepository.findById(productId).get();

        Cart cartStatus = cartRepository.findByUserIdAndProductId(userId, productId);

        Cart cart = null;

        if (ObjectUtils.isEmpty(cartStatus)) {
            cart = new Cart();
            cart.setProduct(product);
            cart.setUser(user);
            cart.setQuantite(1);
            cart.setPrixTotal(1 * product.getPrice());
            cartRepository.save(cart);
        } else {
            cart = cartStatus;
            cart.setQuantite(cart.getQuantite() + 1);
            cart.setPrixTotal(cart.getQuantite() * cart.getProduct().getPrice());
        }
        Cart savedCart = cartRepository.save(cart);
        return savedCart;
    }

    @Override
    public List<Cart> getCartByUser(Long userId) {
        List<Cart> carts = cartRepository.findByUserId(userId);

        int totalOrderPrice = 0;
        List<Cart> updateCarts = new ArrayList<>();
        for (Cart c : carts) {
            int prixTotal = c.getProduct().getPrice() * c.getQuantite();
            c.setPrixTotal(prixTotal);
            totalOrderPrice += prixTotal;

            c.setTotalOrderPrice(totalOrderPrice);
            updateCarts.add(c);
        }

        return updateCarts;
    }

    @Override
    public Integer getCountCart(Long userId) {
        return cartRepository.countByUserId(userId);
    }

    @Override
    public void updateCartQuantity(String sy, Long cartId) {
        Cart cart = cartRepository.findById(cartId).orElseThrow(() -> new RuntimeException("Cart not found"));
        int updatedQuantite;
        if (sy.equalsIgnoreCase("de")) {
            updatedQuantite = cart.getQuantite() - 1;
            if (updatedQuantite <= 0) {
                cartRepository.delete(cart);
            } else {
                cart.setQuantite(updatedQuantite);
                cartRepository.save(cart);
            }
        } else {
            updatedQuantite = cart.getQuantite() + 1;
            cart.setQuantite(updatedQuantite);
            cartRepository.save(cart);
        }
    }
}
