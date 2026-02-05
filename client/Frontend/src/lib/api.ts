const API_BASE = 'http://localhost:5242';

export async function getEvents() {
  const res = await fetch(`${API_BASE}/events`);
  if (!res.ok) throw new Error('Failed to load events');
  return res.json();
}

export async function createEvent(event: {
  title: string;
  description: string;
  date: string;
  categoryId: number;
}) {
  const res = await fetch(`${API_BASE}/events`, {
    method: 'POST',
    headers: {
      'Content-Type': 'application/json'
    },
    body: JSON.stringify(event)
  });

  if (!res.ok) throw new Error('Failed to create event');
  return res.json();
}

export async function updateEvent(
  id: number,
  event: {
    title: string;
    description: string;
    date: string;
    categoryId: number;
  }
) {
  const res = await fetch(`${API_BASE}/events/${id}`, {
    method: 'PUT',
    headers: {
      'Content-Type': 'application/json'
    },
    body: JSON.stringify(event)
  });

  if (!res.ok) {
    throw new Error('Failed to update event');
  }

  return;
}

export async function deleteEvent(id: number) {
  const res = await fetch(`${API_BASE}/events/${id}`, {
    method: 'DELETE'
  });

  if (!res.ok) {
    throw new Error('Failed to delete event');
  }

  return;
}

export async function getCategories() {
  const res = await fetch(`${API_BASE}/categories`);
  if (!res.ok) throw new Error('Failed to load events');
  return res.json();
}

export async function createCategory(category: {
  name: string;
}) {
  const res = await fetch(`${API_BASE}/categories`, {
    method: 'POST',
    headers: {
      'Content-Type': 'application/json'
    },
    body: JSON.stringify(category)
  });

  if (!res.ok) throw new Error('Failed to create category');
  return res.json();
}

export async function updateCategory(
  id: number,
  category: {
    name: string;
  }
) {
  const res = await fetch(`${API_BASE}/categories/${id}`, {
    method: 'PUT',
    headers: {
      'Content-Type': 'application/json'
    },
    body: JSON.stringify(category)
  });

  if (!res.ok) {
    throw new Error('Failed to update category');
  }

  return;
}

export async function deleteCategory(id: number) {
  const res = await fetch(`${API_BASE}/categories/${id}`, {
    method: 'DELETE'
  });

  if (!res.ok) {
    throw new Error('Failed to delete category');
  }

  return;
}