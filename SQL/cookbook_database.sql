CREATE DATABASE IF NOT EXISTS cookbook_database;

CREATE USER cookbook_user IDENTIFIED BY "cookbook_password";
GRANT SELECT, INSERT, UPDATE, DELETE, EXECUTE ON cookbook_database.* TO cookbook_user;

USE cookbook_database;

CREATE TABLE IF NOT EXISTS category(
category_id INT PRIMARY KEY AUTO_INCREMENT,
category_name VARCHAR(45) NOT NULL
);

CREATE TABLE IF NOT EXISTS recipe(
recipe_id INT PRIMARY KEY AUTO_INCREMENT,
category_id INT NOT NULL,
recipe_title VARCHAR(100) NOT NULL,
FOREIGN KEY (category_id) REFERENCES category(category_id)
); 

CREATE TABLE IF NOT EXISTS instruction(
instruction_id INT PRIMARY KEY AUTO_INCREMENT,
recipe_id INT NOT NULL,
instruction_description VARCHAR(200),
FOREIGN KEY (recipe_id) REFERENCES recipe(recipe_id)
);

CREATE TABLE IF NOT EXISTS ingredient(
ingredient_id INT PRIMARY KEY AUTO_INCREMENT,
ingredient_name VARCHAR(45) NOT NULL 
);

CREATE TABLE IF NOT EXISTS unit(
unit_id INT PRIMARY KEY AUTO_INCREMENT,
unit_name VARCHAR(45) NOT NULL
);

Create TABLE IF NOT EXISTS recipe_ingredient(
recipe_id INT NOT NULL,
ingredient_id INT NOT NULL,
unit_id INT,
quantity FLOAT,
PRIMARY KEY (recipe_id, ingredient_id, unit_id),
FOREIGN KEY (recipe_id) REFERENCES recipe(recipe_id),
FOREIGN KEY (ingredient_id) REFERENCES ingredient(ingredient_id),
FOREIGN KEY (unit_id) REFERENCES unit(unit_id)
);

INSERT INTO category (category_name) VALUES
("Breakfast"),
("Starter"),
("Main Course"),
("Dessert"),
("Drinks"),
("Other");

INSERT INTO unit (unit_name) VALUES
("ml"),
("g"),
("tsp"),
("tbsp"),
("pc"),
("no unit");

-- Default recipes
INSERT INTO recipe (category_id, recipe_title) VALUES
(1, "Vegetable Omelette"),
(1, "Greek Yogurt Parfait"),
(1, "Spinach and Feta Frittata"),
(1, "Avocado Toast"),
(1, "Pancakes with Maple Syrup"),
(2, "Caprese Salad Skewers"),
(2, "Stuffed Mushrooms"),
(2, "Bruschetta with Avocado"),
(2, "Spinach and Ricotta Filo Parcels"),
(3, "Eggplant Parmesan"),
(3, "Spinach and Ricotta Stuffed Shells"),
(3, "Mushroom Risotto"),
(3, "Quinoa-Stuffed Bell Peppers"),
(4, "Fruit Tart"),
(4, "Chocolate Mousse"),
(4, "Raspberry Cheesecake"),
(4, "Lemon Sorbet"),
(5, "Classic Mojito"),
(5, "Aperol Spritz"),
(5, "Cosmopolitan"),
(5, "Negroni"),
(5, "Irish Coffee");

INSERT INTO ingredient (ingredient_name) VALUES
("Egg"),
("Bell pepper"),
("Cherry tomatoes"),
("Feta cheese"),
("Olive oil"),
("Greek yogurt"),
("Granola"),
("Strawberries"),
("Blueberries"),
("Fresh mint leaves"),
("Fresh basil leaves"),
("Salt"),
("Pepper"),
("Honey"),
("Spinach"),
("Onion"),
("Red onion"),
("Garlic clove"),
("Whole-grain bread"),
("Avocado"),
("Flour"),
("Baking powder"),
("Sugar"),
("Milk"),
("Butter"),
("Maple syrup"),
("Mozzarella"),
("Mozzarella balls"),
("Balsamic glaze"),
("Mushrooms"),
("Baguette"),
("Sweet potatoes"),
("Breadcrumbs"),
("Parmesan cheese"),
("Filo pastry sheets"),
("Ricotta cheese"),
("Nutmeg"),
("Sesame seeds"),
("Eggplant"),
("Tomato sauce"),
("Jumbo pasta shells"),
("Arborio rice"),
("Vegetable broth"),
("White wine"),
("Quinoa"),
("Black beans"),
("Corn kernels"),
("Lime juice"),
("Shortcrust pastry"),
("Kiwi"),
("Peaches"),
("Custard"),
("Apricot jam"),
("Dark chocolate"),
("Whipping cream"),
("Digestive biscuits"),
("Cream cheese"),
("Sour cream"),
("Raspberry coulis"),
("Water"),
("Ice cubes"),
("Lemon"),
("White rum"),
("Soda water"),
("Aperol"),
("Prosecco"),
("Orange"),
("Vodka"),
("Triple sec"),
("Cranberry juice"),
("Gin"),
("Campari"),
("Sweet vermouth"),
("Irish whisky"),
("Coffee"),
("Sugar syrup");

INSERT INTO instruction (recipe_id, instruction_description) VALUES
(1, "Whisk eggs, add salt and pepper."),
(1, "Sauté chopped bell peppers in olive oil."),
(1, "Pour whisked eggs over peppers."),
(1, "Add halved cherry tomatoes and crumbled feta."),
(1, "Cook until eggs are set."),
(1, "Garnish with fresh basil."),
(2, "Layer Greek yogurt in a glass."),
(2, "Add granola and mixed berries."),
(2, "Repeat layers."),
(2, "Drizzle honey on top and garnish with chopped mint."),
(3, "Sauté chopped onion and garlic in olive oil."),
(3, "Add spinach and cook until wilted."),
(3, "Whisk eggs, add salt, and pepper."),
(3, "Pour eggs over spinach and crumble feta on top."),
(3, "Bake until set."),
(4, "Toast bread slices."),
(4, "Mash avocado and spread on toast."),
(4, "Top with sliced cherry tomatoes and feta."),
(4, "Drizzle with olive oil and season with salt and pepper."),
(5, "Mix flour, baking powder, and sugar."),
(5, "Whisk egg and milk, add to dry ingredients."),
(5, "Stir until smooth."),
(5, "Heat butter in a pan, pour batter."),
(5, "Cook until bubbles form, flip."),
(5, "Serve with maple syrup."),
(6, "Thread cherry tomatoes, mozzarella balls, and basil onto skewers."),
(6, "Drizzle with olive oil and balsamic glaze."),
(6, "Sprinkle with salt and pepper."),
(7, "Remove mushroom stems and chop."),
(7, "Sauté chopped stems, onion, and garlic in olive oil."),
(7, "Add spinach and cook until wilted."),
(7, "Fill mushrooms with the mixture and crumble feta on top."),
(7, "Bake until mushrooms are tender."),
(8, "Toast baguette slices."),
(8, "Mash avocados and spread on the bread."),
(8, "Top with diced tomatoes, red onion, and minced garlic."),
(8, "Drizzle with olive oil and garnish with basil leaves."),
(8, "Season with salt and pepper."),
(9, "Mix spinach, ricotta, and nutmeg."),
(9, "Place filling in the center, fold into parcels."),
(9, "Brush with melted butter, sprinkle sesame seeds."),
(9, "Bake until golden."),
(10, "Slice eggplants, sprinkle with salt, and let sit."),
(10, "Rinse and pat dry, then fry in olive oil until golden."),
(10, "Layer in a baking dish: eggplant, tomato sauce, mozzarella, parmesan, and basil."),
(10, "Repeat layers and bake until bubbly."),
(11, "Cook pasta shells according to package instructions."),
(11, "Mix ricotta, chopped spinach, minced garlic, and egg."),
(11, "Stuff shells, place in a baking dish, cover with tomato sauce and parmesan."),
(11, "Bake until heated through."),
(12, "Sauté chopped onion and mushrooms in butter."),
(12, "Add arborio rice, stir until coated."),
(12, "Pour in white wine, then gradually add hot vegetable broth."),
(12, "Cook until rice is creamy and stir in parmesan."),
(13, "Cook quinoa in vegetable broth."),
(13, "Mix with black beans, corn, diced avocado and lime juice."),
(13, "Stuff bell peppers with the quinoa mixture."),
(13, "Bake until peppers are tender."),
(14, "Roll out pastry and line a tart pan."),
(14, "Bake until golden and let it cool."),
(14, "Spread custard over the pastry."),
(14, "Arrange sliced fruits on top."),
(14, "Heat apricot jam and brush over fruits for a glaze."),
(15, "Melt chocolate and let it cool slightly."),
(15, "Separate egg yolks and whites."),
(15, "Beat egg yolks with sugar."),
(15, "Fold melted chocolate into egg yolks."),
(15, "Whip egg whites until stiff and fold into the mixture."),
(15, "Whip cream and fold it in, let it cool before serving."),
(16, "Crush biscuits, mix with melted butter, and press into a pan."),
(16, "Beat cream cheese and sugar until smooth."),
(16, "Fold in sour cream."),
(16, "Pour over the biscuit base and refrigerate."),
(16, "Drizzle with raspberry coulis before serving."),
(17, "Make a simple syrup with water and sugar."),
(17, "Add lemon juice and zest."),
(17, "Freeze the mixture, stirring occasionally."),
(17, "Once frozen, blend until smooth."),
(17, "Refreeze and serve."),
(18, "Muddle mint leaves with sugar and lime juice."),
(18, "Add white rum and ice cubes."),
(18, "Top up with soda water and stir gently."),
(18, "Garnish with mint sprigs."),
(19, "Fill a glass with ice cubes."),
(19, "Add Aperol and prosecco."),
(19, "Top up with soda water and stir."),
(19, "Garnish with an orange slice."),
(20, "Shake vodka, triple sec, cranberry juice, and lime juice with ice."),
(20, "Strain into a martini glass."),
(20, "Garnish with an orange peel twist."),
(21, "Mix gin, campari, and sweet vermouth."),
(21, "Pour over ice in a glass."),
(21, "Garnish with an orange slice."),
(22, "Mix whisky, hot coffee, and sugar syrup."),
(22, "Top with whipped cream.");

INSERT INTO recipe_ingredient (recipe_id, ingredient_id, unit_id, quantity) VALUES
(1, 1, 5, 3),
(1, 2, 2, 50),
(1, 3, 2, 50),
(1, 4, 2, 30),
(1, 11, 6, NULL),
(1, 5, 6, NULL),
(1, 12, 6, NULL),
(1, 13, 6, NULL),
(2, 6, 2, 200),
(2, 7, 2, 30),
(2, 8, 2, 25),
(2, 9, 2, 25),
(2, 10, 6, NULL),
(2, 14, 6, NULL),
(3, 1, 5, 4),
(3, 15, 2, 100),
(3, 4, 2, 50),
(3, 16, 5, 1),
(3, 18, 5, 1),
(3, 5, 6, NULL),
(3, 12, 6, NULL),
(3, 13, 6, NULL),
(4, 19, 5, 2),
(4, 20, 5, 1),
(4, 3, 6, NULL),
(4, 4, 6, NULL),
(4, 5, 6, NULL),
(4, 12, 6, NULL),
(4, 13, 6, NULL),
(5, 21, 2, 200),
(5, 22, 3, 2),
(5, 23, 4, 1),
(5, 1, 5, 1),
(5, 24, 1, 250),
(5, 25, 6, NULL),
(5, 26, 6, NULL),
(6, 3, 2, 200),
(6, 28, 2, 200),
(6, 11, 6, NULL),
(6, 29, 6, NULL ),
(6, 5, 6, NULL),
(6, 12, 6, NULL),
(6, 13, 6, NULL),
(7, 30, 5, 8),
(7, 15, 2, 150),
(7, 4, 2, 100),
(7, 16, 5, 1),
(7, 18, 5, 2),
(7, 5, 6, NULL),
(7, 12, 6, NULL),
(7, 13, 6, NULL),
(8, 31, 5, 1),
(8, 20, 5, 2),
(8, 3, 6, NULL),
(8, 17, 6, NULL),
(8, 18, 5, 1),
(8, 5, 6, NULL),
(8, 11, 6, NULL),
(8, 12, 6, NULL),
(8, 13, 6, NULL),
(9, 35, 6, NULL),
(9, 15, 2, 200),
(9, 36, 2, 150),
(9, 37, 6, NULL),
(9, 25, 6, NULL),
(9, 38, 6, NULL),
(10, 39, 5, 2),
(10, 40, 2, 400),
(10, 27, 2, 200),
(10, 34, 2, 100),
(10, 11, 6, NULL),
(10, 5, 6, NULL),
(10, 12, 6, NULL),
(10, 13, 6, NULL),
(11, 41, 2, 250),
(11, 36, 2, 300),
(11, 15, 2, 200),
(11, 40, 2, 400),
(11, 1, 5, 1),
(11, 18, 5, 1),
(11, 34, 2, 50),
(12, 42, 2, 300),
(12, 30, 2, 200),
(12, 16, 5, 1),
(12, 43, 1, 500),
(12, 44, 1, 100),
(12, 34, 2, 50),
(12, 25, 6, NULL),
(13, 2, 5, 5),
(13, 45, 2, 200),
(13, 43, 1, 400),
(13, 46, 2, 200),
(13, 47, 2, 200),
(13, 20, 5, 1),
(13, 48, 6, NULL),
(14, 49, 2, 300),
(14, 8, 2, 50),
(14, 9, 2, 50),
(14, 50, 2, 50),
(14, 51, 2, 50),
(14, 52, 1, 200),
(14, 53, 6, NULL),
(15, 54, 2, 200),
(15, 1, 5, 4),
(15, 23, 2, 50),
(15, 55, 1, 150),
(16, 56, 2, 200),
(16, 25, 2, 100),
(16, 57, 2, 400),
(16, 23, 2, 100),
(16, 58, 1, 200),
(16, 59, 6, NULL),
(17, 60, 1, 300),
(17, 23, 2, 150),
(17, 62, 5, 3),
(18, 63, 1, 50),
(18, 48, 1, 30),
(18, 23, 3, 2),
(18, 10, 6, NULL),
(18, 64, 6, NULL),
(18, 61, 6, NULL),
(19, 65, 1, 60),
(19, 66, 1, 90),
(19, 67, 5, 1),
(19, 64, 6, NULL),
(19, 61, 6, NULL),
(20, 68, 1, 40),
(20, 69, 1, 20),
(20, 70, 1, 30),
(20, 48, 1, 10),
(20, 67, 5, 1),
(20, 61, 6, NULL),
(21, 71, 1, 30),
(21, 72, 1, 30),
(21, 73, 1, 30),
(21, 67, 5, 1),
(21, 61, 6, NULL),
(22, 74, 1, 40),
(22, 75, 1, 120),
(22, 76, 1, 20),
(22, 55, 6, NULL);

-- Indexes
CREATE FULLTEXT INDEX idx_recipe_title ON recipe(recipe_title);
CREATE FULLTEXT INDEX idx_ingredient_name ON ingredient(ingredient_name);

-- Views
-- View of ingredients in a recipe
CREATE VIEW recipe_ingredients_overview AS
SELECT
    r.recipe_id,
    r.recipe_title,
    i.ingredient_name,
    ri.quantity,
    u.unit_name
FROM
    recipe r
    LEFT JOIN recipe_ingredient ri ON r.recipe_id = ri.recipe_id
    LEFT JOIN ingredient i ON ri.ingredient_id = i.ingredient_id
    LEFT JOIN unit u ON ri.unit_id = u.unit_id;

-- View of Instructions in a recipe
CREATE VIEW recipe_instructions_overview AS
SELECT
    r.recipe_id,
    r.recipe_title,
    ins.instruction_description
FROM
    recipe r
    LEFT JOIN instruction ins ON r.recipe_id = ins.recipe_id;
    
-- View to display the categories
CREATE VIEW all_categories AS
SELECT category_id, category_name 
FROM category;

-- View to display units
CREATE VIEW all_units AS
SELECT unit_id, unit_name
FROM unit;

-- Stored procedures
-- Search for matching ingredients
DELIMITER $$
CREATE PROCEDURE SearchIngredient(
	IN search_term_ingredient VARCHAR(45)
)
BEGIN
	SELECT ingredient_name
	FROM ingredient
	WHERE MATCH (ingredient_name) AGAINST (CONCAT("+", search_term_ingredient, "*") IN BOOLEAN MODE);
END $$
DELIMITER ;

-- Add an ingredient to the ingredient table
DELIMITER $$
CREATE PROCEDURE AddIngredient(
	IN ingredient_name_param VARCHAR(45)
)
BEGIN
	INSERT INTO ingredient (ingredient_name)
    VALUES (ingredient_name_param);
END $$
DELIMITER ;

-- Check if ingredient already exists, to prevent duplicates
DELIMITER $$
CREATE PROCEDURE IngredientExists(
    IN ingredient_name_param VARCHAR(45)
)
BEGIN
    SELECT COUNT(*) FROM ingredient 
    WHERE ingredient_name = ingredient_name_param;
END $$
DELIMITER ;

-- Delete an ingredient
DELIMITER $$
CREATE PROCEDURE DeleteIngredient(
	IN ingredient_name_param VARCHAR(45)
)
BEGIN
	DELETE FROM ingredient 
    WHERE ingredient_name = ingredient_name_param;
END $$
DELIMITER ;

-- Check if ingredient belongs to a recipe
DELIMITER $$
CREATE PROCEDURE CheckIngredientUsage(
	IN ingredient_name_param VARCHAR(45)
)
BEGIN
	SELECT COUNT(*) AS usage_count
    FROM recipe_ingredient ri
    JOIN ingredient i ON ri.ingredient_id = i.ingredient_id
    WHERE i.ingredient_name = ingredient_name_param;
END $$
DELIMITER ;

-- Search for matching recipes filtered by category
DELIMITER $$
CREATE PROCEDURE SearchRecipe(
	IN search_term_recipe VARCHAR(100),
    IN category_name_param VARCHAR(45)
)
BEGIN
	SELECT r.recipe_title
	FROM recipe r
    JOIN category c ON r.category_id = c.category_id
	WHERE MATCH (r.recipe_title) AGAINST (CONCAT("+", search_term_recipe, "*") IN BOOLEAN MODE)
	AND c.category_name = category_name_param;
END $$
DELIMITER ;

-- Match category name with id
DELIMITER $$
CREATE PROCEDURE GetCategoryId(
	IN category_name_param VARCHAR(45)
)
BEGIN
	SELECT category_id
    FROM category
    WHERE category_name = category_name_param;
END $$
DELIMITER ;

-- Match the ingredient name with id
DELIMITER $$
CREATE PROCEDURE GetIngredientId(
	IN ingredient_name_param VARCHAR(45)
)
BEGIN	
	SELECT ingredient_id 
    FROM ingredient
    WHERE ingredient_name = ingredient_name_param;
END $$
DELIMITER ;

-- Insert a new ingredient and return id
DELIMITER $$
CREATE PROCEDURE InsertIngredient(
	IN ingredient_name_param VARCHAR(45)
)
BEGIN
	INSERT INTO ingredient (ingredient_name) VALUES (ingredient_name_param);
    SELECT LAST_INSERT_ID() AS ingredient_id;
END $$
DELIMITER ;

-- Check if a recipe title already exists, to prevent duplicates
DELIMITER $$
CREATE PROCEDURE RecipeTitleExists(
    IN recipe_title_param VARCHAR(100)
)
BEGIN
    SELECT COUNT(*) FROM recipe 
    WHERE recipe_title = recipe_title_param;
END $$
DELIMITER ;

-- Insert a new recipe and return id
DELIMITER $$
CREATE PROCEDURE InsertRecipe(
	IN recipe_title_param VARCHAR(100),
    IN category_id_param INT
) 
BEGIN
	INSERT INTO recipe (recipe_title, category_id) VALUES (recipe_title_param, category_id_param);
    SELECT LAST_INSERT_ID() AS recipe_id;
END $$
DELIMITER ;

-- Insert instructions to a recipe
DELIMITER $$
CREATE PROCEDURE InsertInstruction(
	IN recipe_id_param INT,
    IN instruction_description_param VARCHAR(200)
)
BEGIN
	INSERT INTO instruction (recipe_id, instruction_description)
    VALUES (recipe_id_param, instruction_description_param);
END $$
DELIMITER ;

-- Insert a new recipe-ingredient association
DELIMITER $$
CREATE PROCEDURE InsertRecipeIngredient(
	IN recipe_id_param INT,
    IN ingredient_id_param INT,
    IN quantity_param FLOAT,
    IN unit_param VARCHAR(45)
)
BEGIN 
	INSERT INTO recipe_ingredient (recipe_id, ingredient_id, quantity, unit_id)
    VALUES (recipe_id_param, ingredient_id_param, quantity_param, (SELECT unit_id FROM unit WHERE unit_name = unit_param));
END $$
DELIMITER ;

-- Delete a recipes ingredients
DELIMITER $$
CREATE PROCEDURE DeleteRecipeIngredient(
	IN recipe_id_param INT
)
BEGIN
	DELETE FROM recipe_ingredient
    WHERE recipe_id = recipe_id_param;
END $$
DELIMITER ;

-- Delete a recipes instructions
DELIMITER $$
CREATE PROCEDURE DeleteRecipeInstruction(
	IN recipe_id_param INT
)
BEGIN
	DELETE FROM instruction
    WHERE recipe_id = recipe_id_param;
END $$
DELIMITER ;

-- Match the recipe name with id
DELIMITER $$
CREATE PROCEDURE GetRecipeId(
	IN recipe_title_param VARCHAR (100)
)
BEGIN
	SELECT recipe_id FROM recipe
    WHERE recipe_title = recipe_title_param;
END $$
DELIMITER ;

-- Update an existing recipe
DELIMITER $$
CREATE PROCEDURE UpdateRecipe(
	IN recipe_id_param INT,
    IN recipe_title_param VARCHAR(100),
    IN category_id_param INT
)
BEGIN
	UPDATE recipe
    SET recipe_title = recipe_title_param,
		category_id = category_id_param
	WHERE recipe_id = recipe_id_param;
END $$
DELIMITER ;

-- Delete recipe
DELIMITER $$
CREATE PROCEDURE DeleteRecipe(
    IN recipe_title_param VARCHAR(100)
)
BEGIN
	DECLARE recipe_id_param INT;
    SELECT recipe_id INTO recipe_id_param 
    FROM recipe 
    WHERE recipe_title = recipe_title_param LIMIT 1;
    DELETE FROM instruction WHERE recipe_id = recipe_id_param;
    DELETE FROM recipe_ingredient WHERE recipe_id = recipe_id_param;
    DELETE FROM recipe
    WHERE recipe_title = recipe_title_param;
END $$
DELIMITER ;





