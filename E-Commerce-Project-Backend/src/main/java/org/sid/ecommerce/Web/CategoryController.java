package org.sid.ecommerce.Web;

import org.sid.ecommerce.Entities.Category;
import org.sid.ecommerce.Service.CategoryService;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.*;

import java.util.List;

@RestController
@RequestMapping("/api/categories")
public class CategoryController {
    @Autowired
    private CategoryService categoryService;

    @PostMapping("/ajoutCat")
    public ResponseEntity<Category> createCategory(@RequestBody Category category) {
        Category createdCategory = categoryService.saveCategory(category);
        return ResponseEntity.ok(createdCategory);
    }

    @GetMapping("/affiche/{id}")
    public ResponseEntity<Category> getCategoryById(@PathVariable Long id) {
        Category category = categoryService.getCategoryById(id);
        return category != null ? ResponseEntity.ok(category) : ResponseEntity.<Category>notFound().build();
    }

    @GetMapping("/afficheTous")
    public List<Category> getAllCategories() {
        return categoryService.getAllCategories();
    }

    @PutMapping("/update/{id}")
    public ResponseEntity<Category> updateCategory(@PathVariable Long id, @RequestBody Category category) {
        Category updatedCategory = categoryService.updateCategory(id, category);
        return updatedCategory != null ? ResponseEntity.ok(updatedCategory) : ResponseEntity.<Category>notFound().build();
    }

    @DeleteMapping("/supprimer/{id}")
    public ResponseEntity<Void> deleteCategory(@PathVariable Long id) {
        categoryService.deleteCategory(id);
        return ResponseEntity.noContent().build();
    }
}
