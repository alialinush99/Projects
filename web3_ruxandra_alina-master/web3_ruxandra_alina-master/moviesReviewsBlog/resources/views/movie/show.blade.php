@extends('layouts.app')



@section('content')

<div class = "text-center">
<a href="{{ url('/movies/')}}"  ><button class="btn btn-danger mt-5">Back to Movies</button></a>

  <head>
  <h3 class="display-4  text-center mt-4">{{$movie->name}} </h3>
  <small class ="mb-3">{{$movie->release_year}}</small>
  </head>

<div>
  <label class ="mb-3">({{$movie->genre}})</label>
  <div>
  <h3 class= "display-5"> Description </h3> <p>{{$movie->description}}</p>
</div>

<div>
  <h3 class= "display-5"> Cast </h3> <p>{{$movie->cast}}</p>
</div>

<div>
  <h3 class= "display-5"> Rating </h3> <p>{{$movie->rating}}</p>
</div>
</div>


</div>
@endsection