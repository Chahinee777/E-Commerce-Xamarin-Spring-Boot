package org.sid.ecommerce.Web;

import org.sid.ecommerce.Entities.Product;
import org.sid.ecommerce.Service.ProductService;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.http.MediaType;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.*;
import org.springframework.web.multipart.MultipartFile;

import java.util.List;

@RestController
@RequestMapping("/api/produits")
public class ProductController {
    @Autowired
    private ProductService productService;

    @PostMapping("/ajoutProduit")
    public ResponseEntity<Product> createProduct(
            @RequestPart("product") Product product,
            @RequestPart("Imageile") MultipartFile Imageile) {
        try {
            Product createdProduct = productService.saveProduct(product, Imageile);
            return ResponseEntity.ok(createdProduct);
        } catch (Exception e) {
            return ResponseEntity.badRequest().build();
        }
    }

    @GetMapping("/image/{productId}")
    public ResponseEntity<byte[]> getImageByProductId(@PathVariable Long productId) {
        Product product = productService.getProductById(productId);
        if (product != null && product.getImageDate() != null) {
            return ResponseEntity.ok()
                    .contentType(MediaType.valueOf(product.getImageType()))
                    .body(product.getImageDate());
        } else {
            return ResponseEntity.notFound().build();
        }
    }

    @GetMapping("/affiche/{id}")
    public ResponseEntity<Product> getProductById(@PathVariable Long id) {
        Product product = productService.getProductById(id);
        return product != null ? ResponseEntity.ok(product) : ResponseEntity.notFound().build();
    }

    @GetMapping("/afficheTous")
    public List<Product> getAllProducts() {
        return productService.getAllProducts();
    }

    @PutMapping("/update/{id}")
    public ResponseEntity<Product> updateProduct(@PathVariable Long id, @RequestBody Product product) {
        Product updatedProduct = productService.updateProduct(id, product);
        return updatedProduct != null ? ResponseEntity.ok(updatedProduct) : ResponseEntity.notFound().build();
    }

    @DeleteMapping("/supprimer/{id}")
    public ResponseEntity<Void> deleteProduct(@PathVariable Long id) {
        productService.deleteProduct(id);
        return ResponseEntity.noContent().build();
    }
}
