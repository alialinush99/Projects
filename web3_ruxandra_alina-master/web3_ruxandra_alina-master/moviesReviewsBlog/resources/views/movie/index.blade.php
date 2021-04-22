@extends('layouts.app')
@section('content')

<head ><h3 class="display-4 text-center mb-3 mt-3">Movies List</h3>
<p class = "text-center mb-5">In this section, you can see a list of all the movies we have on this website </p>
</head>
  <body>
  <table class="table">
  <thead>
    <tr>
      <th scope="col">Name</th>
      <th scope="col">Description</th>
      <th scope="col">Genre</th>
      
      <th scope="col"></th>

    </tr>
  </thead>
  <tbody>
  @foreach ($movies as $movie)
    <tr>
      <td>{{ $movie->name }}</td>
      <td>{{$movie->description = substr($movie->description, 0, 30) . '...' }}</td>
      <td>{{ $movie->genre }}</td>
     

      <td><a href="{{ url('/movies/' . $movie->id) }}" ><button class="btn btn-success">See more</button></a></td>

    </tr>
  @endforeach
  </tbody>
</table>
@endsection












