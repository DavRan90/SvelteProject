<script lang="ts">
    import EventItem from "./EventItem.svelte";

    interface Event {
        id: number;
        title: string;
        description: string;
        date: string;
        editing: boolean;
    }

    export let events: Event[];
    export let onDelete: (id: number) => void;
    export let onEdit: (id: number, event: Event) => void;

</script>

<div>
    <ul>
        {#each events as event(event.id)}
        <li>
            <EventItem {event} onDelete={onDelete} onEdit={onEdit}/>
        </li>
        {/each}
    </ul>
</div>

<style>
    ul {
        list-style: none;
        padding: 0;
        width: 250px;
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
</style>