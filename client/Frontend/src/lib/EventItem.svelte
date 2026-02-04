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

    function handleDelete(){
        onDelete(event.id);
    }

    function handleEdit(){
        event.editing = !event.editing;
    }

    function saveEdit(){
        event.editing = !event.editing;
        onEdit(event.id, event);
    }

    let categories: Category[] = [];
        
    onMount(async () => {
        categories = await getCategories();
    });

</script>


<div>
    <div class="grid">
        {#if !event.editing}
            <p>{event.title}</p>
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
                        <select
                            bind:value={event.categoryId}
                            onchange={(e) => {
                                event.categoryId = Number(e.currentTarget.value);
                                console.log('categoryId:', event.categoryId, typeof event.categoryId);
                            }}
                        >
                            <option value={null}>No category</option>
                            {#each categories as category}
                                <option value={category.id}>
                                {category.name}
                                </option>
                            {/each}
                            </select>
                    </label>
                </div>
                <div>
                    <button type="submit">Save</button>
                </div>
            </form>
        </div>
            <button class="disabled emoji">✏️</button>
        {/if}
        <button onclick={handleDelete} class="emoji">🗑️</button>
    </div>
</div>



<style>
    p {
        margin-left: 1em;
    }
    button {
        background-color: aliceblue;
        border-radius: 5px;
        padding: 1em;
    }
    input{
        margin: 1em;
    }

    .grid{
        display: grid;
		grid-template-columns: 15em 3em 3em;
		grid-gap: 1em;
		height: 100%;
    }
    input{
        border-radius: 10px;
        padding: 1em;
    }
    .emoji{
        background-color: aliceblue;
        border-radius: 5px;
        padding: 1em;
        max-width: 4em;
        max-height: 4em;
    }
    .disabled{
        background-color: grey;
    }

</style>