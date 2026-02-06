<script lang="ts">
    import { onMount } from 'svelte';
    import { getCategories, createCategory, deleteCategory, updateCategory} from '$lib/api';
    import CategoryList from '$lib/CategoryList.svelte';

    interface Category {
        id: number;
        name: string;
    }

    let categories: Category[] = $state([]);
    let newCategory = $state({name: "New Category"});


    onMount(async () => {
    categories = await getCategories();
  });

  async function addCategory() {
            await createCategory({
                name: newCategory.name
                });
            categories = await getCategories();
            newCategory = {name: ""}
        }
    async function onDelete(id: number) {
      try {
        await deleteCategory(id);
        categories = await getCategories();
      } catch (err) {
        if (err instanceof Error) {
          alert(err.message);
        } else {
          alert('Something went wrong');
        }
      }
    }

    async function onUpdate(id: number, category: Category) {
        await updateCategory(id, {
        name: category.name,
        });
        categories = await getCategories();
    }

</script>


<div class="grid">
  <form onsubmit={addCategory}>
    <div>
      <h2>New category</h2>
        <label>
            Title
            <input type="text" bind:value={newCategory.name}/>
        </label>
    </div>
    <button type="submit">Create</button>
  </form>
</div>

<div>
    <h2>Categories</h2>
    <CategoryList {categories} onDelete={onDelete} onEdit={onUpdate}/>
</div>


<style>

  h2 {
    margin-bottom: 0.5rem;
  }

  form {
    background: white;
    padding: 1rem;
    border-radius: 8px;
    max-width: 420px;
    box-shadow: 0 6px 16px rgba(0,0,0,0.08);
    margin-bottom: 2rem;
  }

  form > div {
    margin-bottom: 0.75rem;
  }

  label {
    display: flex;
    flex-direction: column;
    font-size: 0.9rem;
    gap: 0.25rem;
  }

  input {
    padding: 0.5rem 0.6rem;
    border-radius: 6px;
    border: 1px solid #ccc;
    font-size: 1rem;
  }

  input:focus {
    outline: none;
    border-color: #4f46e5;
    box-shadow: 0 0 0 2px rgba(79,70,229,0.15);
  }

  button {
    margin-top: 0.5rem;
    padding: 0.6rem 1rem;
    border-radius: 6px;
    border: none;
    background: #4f46e5;
    color: white;
    font-weight: 600;
    cursor: pointer;
  }

  button:hover {
    background: #4338ca;
  }

</style>