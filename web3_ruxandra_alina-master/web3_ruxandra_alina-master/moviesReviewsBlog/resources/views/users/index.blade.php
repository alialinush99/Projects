@extends('layouts.app')
@section('content')


<head ><h3 class="display-4 text-center mb-3 mt-3">Users List</h3>
<p class = "text-center mb-5">In this section, you can see a list of all users </p>
</head>
  <body>
  <table class="table">
  <thead>
    <tr>
      <th scope="col">Name</th>
      <th scope="col">Email</th>
      <th scope="col">Created At</th>
      <th scope="col">Updated At</th>
      
      <th scope="col"></th>

    </tr>
  </thead>
  <tbody>
  @foreach ($users as $user)
    <tr>
      <td>{{ $user->name }}</td>
      <td>{{ $user->email }}</td>
      <td>{{ $user->created_at->diffForHumans() }}</td>
     

      <td><a href="{{ url('/users/' . $user->id) }}" ><button class="btn btn-success">See more</button></a></td>

    </tr>
  @endforeach
  </tbody>
</table>

@endsection