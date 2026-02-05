<script lang="ts">

    interface EventDto {
        id: number;
        title: string;
        description: string;
        date: string;
        editing: boolean;
        categoryId: number;
    }
    interface Category {
        id: number;
        name: string;
    }

    import { getCategories } from '$lib/api';
    import { onMount } from 'svelte';

    export let event: EventDto;
    export let onDelete: (id: number) => void;
    export let onEdit: (id: number, event: EventDto) => void;

    export let editing: boolean;
	export let onEditStart: () => void;
	export let onEditDone: () => void;

    function handleDelete(){
        onDelete(event.id);
    }

    function handleEdit(){
        editing = !editing;
        onEditStart();
    }

    function saveEdit(){
        event.editing = !event.editing;
        onEdit(event.id, event);
        onEditDone();
    }

    let categories: Category[] = [];
        
    onMount(async () => {
        categories = await getCategories();
    });

</script>


<div class="grid">
        {#if !editing}
            <p onclick={handleEdit}>{event.title}</p>
            <button onclick={handleEdit}>✏️</button>
        {:else}
        <div>
            <form onsubmit={saveEdit}>
                <div>
                <label>
                    Title
                    <input type="text" bind:value={event.title}/>
                </label>
                </div>
                <div>
                <label>
                    Description
                    <input type="text" bind:value={event.description}/>
                </label>
                </div>
                <div>
                <label>
                    Date
                    <input type="text" bind:value={event.date}/>
                </label>
                </div>
                <div>
                <label>
                    Category
                    <select bind:value={event.categoryId}>
                    <option value={null}>No category</option>

                    {#each categories as category}
                        <option value={category.id}>
                        {category.name}
                        </option>
                    {/each}
                    </select>
                </label>
                </div>
                <button type="submit">Save</button>
            </form>
        </div>
            <button onclick={handleEdit} class="clicked">✏️</button>
        {/if}
        <button onclick={handleDelete}>🗑️</button>
</div>



<style>
    p {
        margin-left: 1em;
    }
    input, select {
        margin-top: 0.5rem;
        padding: 0.6rem 1rem;
        border-radius: 6px;
    }
    button {
        padding: 1rem;
        min-width: 4em;
        min-height: 4em;
        max-height: 4em;
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
    .grid{
        display: grid;
		grid-template-columns: 28em 4em 4em;
		grid-gap: 1em;
		height: 100%;
    }
    .emoji{
        background-color: aliceblue;
        border-radius: 5px;
        padding: 1em;
        max-width: 4em;
        max-height: 4em;
    }
    .clicked{
        background-color: grey;
    }

</style>