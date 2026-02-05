<script lang="ts">
	import favicon from '$lib/assets/favicon.svg';

	let { children } = $props();
</script>

<svelte:head>
	<link rel="icon" href={favicon} />
</svelte:head>
<h2>Eventplanner</h2>
<nav>
	<a href="/">Events</a>
	<a href="/Categories">Categories</a>
</nav>



{@render children()}

<style>
	h2{
		margin-left: 4px;
	}
</style>