<script lang="ts">
    interface Category {
        id: number;
        name: string;
    }

    import { getCategories } from '$lib/api';
    import { onMount } from 'svelte';

    export let category: Category;
    export let onDelete: (id: number) => void;
    export let onEdit: (id: number, category: Category) => void;

    export let editing: boolean;
	export let onEditStart: () => void;
	export let onEditDone: () => void;
    

    function handleDelete(){
        onDelete(category.id);
    }

    function handleEdit(){
        editing = !editing;
        onEditStart();
    }

    function saveEdit(){
        editing = !editing;
        onEdit(category.id, category);
        onEditDone();
    }

    let categories: Category[] = [];
        
    onMount(async () => {
        categories = await getCategories();
    });

</script>

<div class="categories">
    {#if !editing}
		<label onclick={handleEdit}>
			{category.name}
		</label>
	{:else}
		<input 
        type="text" 
        bind:value={category.name}
        onkeydown={(e) => e.key === 'Enter' && saveEdit()}
        >
	{/if}
    <button onclick={handleDelete}>🗑️</button>
</div>

<style>
    input{
        max-width: fit-content;
    }
    .categories{
        display: grid;
		grid-template-columns: 28em 4em 4em;
		grid-gap: 1em;
		height: 100%;
    }
    button {
        padding: 1rem;
        min-width: 3em;
        min-height: 3em;
        max-height: 3em;
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