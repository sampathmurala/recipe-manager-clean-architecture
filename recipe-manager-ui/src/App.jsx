import { useState, useEffect } from 'react';
import './App.css';

const API_URL = 'https://localhost:7000/api/recipes';

function App() {
    const [recipes, setRecipes] = useState([]);
    const [name, setName] = useState('');
    const [instructions, setInstructions] = useState('');
    const [ingredients, setIngredients] = useState([{ name: '', quantity: 0, unit: '' }]);

    // Fetches the list of recipes from the API on component load.
    const fetchRecipes = async () => {
        try {
            const response = await fetch(API_URL);
            if (!response.ok) {
                throw new Error('Network response was not ok');
            }
            const data = await response.json();
            setRecipes(data);
        } catch (error) {
            console.error('Failed to fetch recipes:', error);
        }
    };

    useEffect(() => {
        fetchRecipes();
    }, []);

    // Handles adding a new ingredient to the form.
    const handleAddIngredient = () => {
        setIngredients([...ingredients, { name: '', quantity: 0, unit: '' }]);
    };

    // Handles form submission to create a new recipe.
    const handleSubmit = async (event) => {
        event.preventDefault();
        try {
            const newRecipe = { name, instructions, ingredients };
            const response = await fetch(API_URL, {
                method: 'POST',
                headers: { 'Content-Type': 'application/json' },
                body: JSON.stringify(newRecipe),
            });

            if (!response.ok) {
                throw new Error('Failed to create recipe');
            }

            // Reset the form and refetch the recipes list.
            setName('');
            setInstructions('');
            setIngredients([{ name: '', quantity: 0, unit: '' }]);
            await fetchRecipes();
        } catch (error) {
            console.error('Failed to submit form:', error);
        }
    };

    return (
        <div className="app-container">
            <header className="App-header">
                <h1>Recipe Manager</h1>
            </header>
            <main>
                <div className="grid-container">
                    <div className="create-recipe-form">
                        <h2>Create New Recipe</h2>
                        <form onSubmit={handleSubmit}>
                            <label>
                                Recipe Name:
                                <input type="text" value={name} onChange={(e) => setName(e.target.value)} required />
                            </label>
                            <label>
                                Instructions:
                                <textarea value={instructions} onChange={(e) => setInstructions(e.target.value)} required />
                            </label>
                            <h3>Ingredients</h3>
                            {ingredients.map((ingredient, index) => (
                                <div key={index} className="ingredient-row">
                                    <input
                                        type="text"
                                        placeholder="Ingredient Name"
                                        value={ingredient.name}
                                        onChange={(e) => {
                                            const newIngredients = [...ingredients];
                                            newIngredients[index].name = e.target.value;
                                            setIngredients(newIngredients);
                                        }}
                                        required
                                    />
                                    <input
                                        type="number"
                                        placeholder="Quantity"
                                        value={ingredient.quantity}
                                        onChange={(e) => {
                                            const newIngredients = [...ingredients];
                                            newIngredients[index].quantity = parseFloat(e.target.value);
                                            setIngredients(newIngredients);
                                        }}
                                        required
                                    />
                                    <input
                                        type="text"
                                        placeholder="Unit"
                                        value={ingredient.unit}
                                        onChange={(e) => {
                                            const newIngredients = [...ingredients];
                                            newIngredients[index].unit = e.target.value;
                                            setIngredients(newIngredients);
                                        }}
                                        required
                                    />
                                </div>
                            ))}
                            <button type="button" onClick={handleAddIngredient}>Add Ingredient</button>
                            <button type="submit">Create Recipe</button>
                        </form>
                    </div>
                    <div className="recipes-list">
                        <h2>Existing Recipes</h2>
                        {recipes.length > 0 ? (
                            <ul>
                                {recipes.map((recipe) => (
                                    <li key={recipe.id}>
                                        <h3>{recipe.name}</h3>
                                        <p>{recipe.instructions}</p>
                                        {recipe.ingredients && recipe.ingredients.length > 0 && (
                                            <>
                                                <h4>Ingredients:</h4>
                                                <ul>
                                                    {recipe.ingredients.map((ingredient, index) => (
                                                        <li key={index}>
                                                            {ingredient.quantity} {ingredient.unit} of {ingredient.name}
                                                        </li>
                                                    ))}
                                                </ul>
                                            </>
                                        )}
                                    </li>
                                ))}
                            </ul>
                        ) : (
                            <p>No recipes found. Create one!</p>
                        )}
                    </div>
                </div>
            </main>
        </div>
    );
}

export default App;
