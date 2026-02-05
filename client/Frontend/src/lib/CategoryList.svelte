<script lang="ts">
    import CategoryItem from "./CategoryItem.svelte";

    interface Category {
        id: number;
        name: string;
        editing: boolean;
    }

    export let categories: Category[];
    export let onDelete: (id: number) => void;
    export let onEdit: (id: number, category: Category) => void;

</script>

<div>
    <ul>
        {#each categories as category(category.id)}
        <li>
            <CategoryItem {category} onDelete={onDelete} onEdit={onEdit}/>
        </li>
        {/each}
    </ul>
</div>

<style>
    ul {
        list-style: none;
        padding: 0;
        width: 450px;
        min-height: 250px;
    }
    
    li {
        display: flex;
        gap: 0.5rem;
        align-items: center;
        padding: 0.4rem;
        border-radius: 6px;
        background: #f5f5f5;
        margin-bottom: 0.4rem;
    }
    li:hover {
        background: #e3e3e4;
    }
</style>