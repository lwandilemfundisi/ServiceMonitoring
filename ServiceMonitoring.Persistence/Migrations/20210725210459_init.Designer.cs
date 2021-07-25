﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ServiceMonitoring.Persistence;

namespace ServiceMonitoring.Persistence.Migrations
{
    [DbContext(typeof(ServiceMonitorContext))]
    [Migration("20210725210459_init")]
    partial class init
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.8")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("ServiceMonitoring.Domain.DomainModel.ServiceMonitoringDomainModel.Entities.ServiceMethod", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ExecutedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("ExecutionTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("ExecutionsStatus")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Request")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Response")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ServiceMonitorAggregateId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<TimeSpan>("TimeElapsed")
                        .HasColumnType("time");

                    b.HasKey("Id");

                    b.HasIndex("ServiceMonitorAggregateId");

                    b.ToTable("ServiceMethod");
                });

            modelBuilder.Entity("ServiceMonitoring.Domain.DomainModel.ServiceMonitoringDomainModel.ServiceMonitorAggregate", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ServiceName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Version")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("ServiceMonitorAggregate");
                });

            modelBuilder.Entity("ServiceMonitoring.Domain.DomainModel.ServiceMonitoringDomainModel.Entities.ServiceMethod", b =>
                {
                    b.HasOne("ServiceMonitoring.Domain.DomainModel.ServiceMonitoringDomainModel.ServiceMonitorAggregate", null)
                        .WithMany("ServiceMethods")
                        .HasForeignKey("ServiceMonitorAggregateId");
                });

            modelBuilder.Entity("ServiceMonitoring.Domain.DomainModel.ServiceMonitoringDomainModel.ServiceMonitorAggregate", b =>
                {
                    b.Navigation("ServiceMethods");
                });
#pragma warning restore 612, 618
        }
    }
}