<script lang="ts">
    import { getCategories} from '$lib/api';
    import { onMount } from 'svelte';
    import CategoryItem from "../CategoryItem.svelte";

    interface Category {
        id: number;
        name: string;
    }

    let categories: Category[] = [];

    onMount(async () => {
    categories = await getCategories();
  });

    export let onDelete: (id: number) => void;
    export let onEdit: (id: number, category: Category) => void;
    let editingCategoryId: number | null = null;



</script>

<div>
    <ul>
        {#each categories as category (category.id)}
        <li>
	    <CategoryItem
            {category}
            editing={editingCategoryId === category.id}
            onEditStart={() => editingCategoryId = category.id}
            onEditDone={() => editingCategoryId = null}
            onDelete={onDelete} onEdit={onEdit}
	    />
        </li>
    {/each}
    </ul>
</div>

<style>

</style>