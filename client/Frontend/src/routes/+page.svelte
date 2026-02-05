<script lang="ts">
    import EventList from "$lib/EventList.svelte";
    import { onMount } from 'svelte';
    import { getEvents, createEvent, updateEvent, deleteEvent, getCategories} from '$lib/api';

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

    let categories: Category[] = $state([]);
    let events = $state([]);
    let selectedCategoryId = $state(0);
    let newEvent = $state({title: "Default title", description: "Default Description", date: "1970-05-01", categoryId: 0});
        
    onMount(async () => {
    events = await getEvents();
    categories = await getCategories();
  });
        async function addEvent() {
            await createEvent({
                title: newEvent.title,
                description: newEvent.description,
                date: newEvent.date,
                categoryId: selectedCategoryId
                });
            events = await getEvents();
            newEvent = {title: "", description: "", date: "", categoryId: 0}
        }

        async function onDelete(id: number){
            await deleteEvent(id);
            events = await getEvents();
        }

        async function onUpdate(id: number, event: EventDto) {
            await updateEvent(id, {
            title: event.title,
            description: event.description,
            date: event.date,
            categoryId: event.categoryId
            });
            events = await getEvents();
        }
</script>

<div class="grid">
  <form onsubmit={addEvent}>
    <div>
      <h2>New event details:</h2>
      <label>
        Title
        <input type="text" bind:value={newEvent.title}/>
      </label>
    </div>
    <div>
      <label>
        Description
        <input type="text" bind:value={newEvent.description}/>
      </label>
    </div>
    <div>
      <label>
        Date
        <input type="text" bind:value={newEvent.date}/>
      </label>
    </div>
    <div>
      <label>
        Category
        <select bind:value={selectedCategoryId}>
          <option value={null}>No category</option>

          {#each categories as category}
            <option value={category.id}>
              {category.name}
            </option>
          {/each}
        </select>
      </label>
    </div>
    <button type="submit">Create</button>
  </form>
</div>

<h2>Events</h2>
<div>
    <EventList {events} onDelete={onDelete} onEdit={onUpdate}/>
</div>

<style>
  h1, h2 {
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